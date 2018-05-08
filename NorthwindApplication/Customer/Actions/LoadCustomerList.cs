using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Ef;
using Redux;

namespace NorthwindApplication.Customer.Actions
{
    
    public class LoadCustomerListAction : IAction 
    {
    }

    public class LoadCustomerListCompleteAction : IAction
    {
        public LoadCustomerListCompleteAction(IList<Customer> customers)
        {
            Customers = customers;
        }
        public IList<Customer> Customers { get; private set; }
    }
    
    
    public class LoadCustomerListEffect : ActionEffect<LoadCustomerListAction, CustomerState>
    {
        private readonly NorthwindContext _dbCtx;
        private readonly ILogger<LoadCustomerListEffect> _logger;

        public LoadCustomerListEffect(NorthwindContext dbCtx, ILogger<LoadCustomerListEffect> logger)
        {
            _dbCtx = dbCtx;
            _logger = logger;
        }

        public override async Task<IAction> Effect(CustomerState prevState, LoadCustomerListAction action)
        {
            _logger.LogInformation("Load Customer List Effect");
            
            var customers =  await _dbCtx.Customers
                .OrderBy(c => c.CustomerId)
                .Select(c => new Customer()
                {
                    Address = c.Address,
                    City = c.City,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    CustomerId = c.CustomerId
                }).ToListAsync();
            
             return new LoadCustomerListCompleteAction(customers);
        }
    }
    
    public class LoadCustomerListCompleteReducer : ActionReducer<LoadCustomerListCompleteAction, CustomerState>
    {
        
        public override CustomerState Reducer(CustomerState prevState, LoadCustomerListCompleteAction action)
        {
            return new CustomerState()
            {
                OpenCustomers = prevState.OpenCustomers,
                CustomerList = action.Customers
            };
        }
    }
}