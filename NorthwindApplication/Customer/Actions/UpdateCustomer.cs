using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Redux;

namespace NorthwindApplication.Customer.Actions
{
    public class UpdateCustomer : IAction
    {
        public UpdateCustomer(Customer customer)
        {
            Customer = customer;
        }   
        public Customer Customer { get; private set; }
    }


    public class UpdateCustomerReducer : ActionReducer<UpdateCustomer, CustomerState>
    {
        public override CustomerState Reducer(CustomerState state, UpdateCustomer action)
        {
            var prevOc = state.OpenCustomers.FirstOrDefault(oc => oc.Customer.CustomerId == action.Customer.CustomerId) 
                         ?? throw new Exception($"customer {action.Customer.CustomerId} is not opened");
            
            var newOc = new OpenedCustomer()
            {
                Customer = new Customer()
                {
                    CustomerId = action.Customer.CustomerId,
                    Address = action.Customer.Address,
                    City = action.Customer.City,
                    CompanyName = action.Customer.CompanyName,
                    ContactName = action.Customer.ContactName
                },
                Users = prevOc.Users,
                IsChanged = prevOc.IsChanged
            };
            state.OpenCustomers.Remove(prevOc);
            state.OpenCustomers.Add(newOc);
            return state;
        }
    }
}