using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Redux;

namespace NorthwindApplication.Customer
{
    public class ActionEffectMiddleware
    {
        private readonly IContainer _container;

        public ActionEffectMiddleware(IContainer container)
        {
            _container = container;
        }

        public Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store)
        {
            return (Dispatcher next) => (IAction action) =>
            {
                var toReturn = next(action); // complete state change first


                var genericEffectType = typeof(ActionEffect<,>).MakeGenericType(action.GetType(), typeof(TState));
                var allEffectTypes = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => genericEffectType.IsAssignableFrom(t));

                // run all effects in background tasks
                foreach (var effectType in allEffectTypes)
                {
                    Task.Run(() =>
                    {
                        using (var scope = _container.BeginLifetimeScope())
                        {
                            if (scope.IsRegistered(effectType))
                            {
                                var effect = scope.Resolve(effectType) as ActionEffect<TState>;
                                var resultAction = effect?.Effect(store.GetState(), action).GetAwaiter().GetResult(); // run the effect
                                if (resultAction != null) store.Dispatch(resultAction);                                
                            }                            
                        }
                    });
                }       
                return toReturn;
            };
        }
    }
}