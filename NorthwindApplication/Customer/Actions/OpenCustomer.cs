using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Ef;
using Redux;

namespace NorthwindApplication.Customer.Actions
{
    public class OpenCustomer : IAction
    {
        public OpenCustomer(string customerId, string userId)
        {
            CustomerId = customerId;
            UserId = userId;
        }
        public string CustomerId { get; private set; }
        public string UserId { get; private set; }
    }
    
    
    
    public class OpenCustomerEffect : ActionEffect<OpenCustomer, CustomerState>
    {
        private readonly NorthwindContext _dbCtx;
        private readonly ILogger<OpenCustomerEffect> _logger;

        public OpenCustomerEffect(NorthwindContext dbCtx
            , ILogger<OpenCustomerEffect> logger
            )
        {
            _dbCtx = dbCtx;
            _logger = logger;
        }


        public override async Task<IAction> Effect(CustomerState prevState, OpenCustomer action)
        {
            _logger.LogInformation("OpenCustomerEffect {CustomerId} {userId}", action.CustomerId, action.UserId );
            var customer = await _dbCtx.Customers
                .Select(c => new Customer()
                {
                    Address = c.Address,
                    City = c.City,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    CustomerId = c.CustomerId
                })
                .FirstOrDefaultAsync(c => c.CustomerId == action.CustomerId);
            return new OpenCustomerSuccess(customer, action.UserId);
        }
    }


   
    
}