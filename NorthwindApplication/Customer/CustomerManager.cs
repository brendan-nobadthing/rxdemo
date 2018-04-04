using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Redux;

namespace NorthwindApplication.Customer
{
    public class CustomerManager : ICustomerManager
    {        
        public Store<CustomerState> CustomerStore { get; private set; }


        public CustomerManager()
        {
            CustomerStore = new Store<CustomerState>(Reducer, new CustomerState(), EffectMiddleware.Middleware);
        }


        private CustomerState Reducer(CustomerState state, IAction action)
        {
            var genericActionReducerType = typeof(ActionReducer<,>).MakeGenericType(action.GetType(), typeof(CustomerState));
            var actionReducerType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => genericActionReducerType.IsAssignableFrom(t));
  
            var actionReducer = actionReducerType != null 
                ? Activator.CreateInstance(actionReducerType) as ActionReducer<CustomerState> 
                : null;

            return actionReducer == null 
                ? state 
                : actionReducer.Reducer(state, action);
        }
        
        
    }


    public static class EffectMiddleware
    {
        public static Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store)
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
                        if (Activator.CreateInstance(effectType) is ActionEffect<TState> effect)
                        {
                            var resultAction = effect.Effect(store.GetState(), action);// run the effect
                            if (resultAction != null) store.Dispatch(resultAction); 
                        }
                    });
                }
                
                
                return toReturn;
            };
        }
    }

}