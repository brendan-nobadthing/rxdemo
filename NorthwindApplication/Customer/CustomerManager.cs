using System;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using Autofac;
using Redux;

namespace NorthwindApplication.Customer
{
    public class CustomerManager
    {        
        public Store<CustomerState> CustomerStore { get; private set; }

        public CustomerManager(IContainer container)
        {
            CustomerStore = new Store<CustomerState>(Reducer, new CustomerState(), new ActionEffectMiddleware(container).Middleware);
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
}