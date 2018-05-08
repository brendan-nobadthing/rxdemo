using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Redux;

namespace NorthwindApplication.Customer.Actions
{
    public class SaveCustomerSuccessful : IAction
    {
        public SaveCustomerSuccessful(Customer customer)
        {
            Customer = customer;
        }
        public Customer Customer { get; private set; }
    }

     
    public class SaveCustomerSuccessfulReloadListEffect : ActionEffect<SaveCustomerSuccessful, CustomerState>
    {
        private readonly ILogger<SaveCustomerSuccessfulReloadListEffect> _logger;

        public SaveCustomerSuccessfulReloadListEffect(ILogger<SaveCustomerSuccessfulReloadListEffect> logger)
        {
            _logger = logger;
        }

        public override Task<IAction> Effect(CustomerState prevState, SaveCustomerSuccessful action)
        {
            _logger.LogInformation("SaveCustomerSuccessfulReloadListEffect");
            return Task.FromResult<IAction>(new LoadCustomerListAction());
        }
    }
    
    
    public class SaveCustomerSuccessfulReducer : ActionReducer<SaveCustomerSuccessful, CustomerState>
    {
        public override CustomerState Reducer(CustomerState state, SaveCustomerSuccessful action)
        {
            var prevOc =
                state.OpenCustomers.FirstOrDefault(oc => oc.Customer.CustomerId == action.Customer.CustomerId)
                ?? throw new Exception($"customer {action.Customer.CustomerId} is not opened");;

            var newOc = new OpenedCustomer()
            {
                Customer = action.Customer,
                Users = prevOc.Users,
                IsChanged = false
            };
            state.OpenCustomers.Remove(prevOc);
            state.OpenCustomers.Add(newOc);
            return state;
        }
    }
}