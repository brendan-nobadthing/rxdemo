using System.Linq;
using System.Threading.Tasks;
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
        public override Task<IAction> Effect(CustomerState prevState, SaveCustomerSuccessful action)
        {
            return Task.FromResult<IAction>(new LoadCustomerListAction());
        }
    }
    
    
    public class SaveCustomerSuccessfulReducer : ActionReducer<SaveCustomerSuccessful, CustomerState>
    {
        public override CustomerState Reducer(CustomerState state, SaveCustomerSuccessful action)
        {
            var openItem =
                state.OpenCustomers.FirstOrDefault(oc => oc.Customer.CustomerId == action.Customer.CustomerId);

            if (openItem != null)
            {
                openItem.Customer = action.Customer;
            }
            return state;
        }
    }
}