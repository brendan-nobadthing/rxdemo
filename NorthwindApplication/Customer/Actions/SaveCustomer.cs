using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Ef;
using Redux;

namespace NorthwindApplication.Customer.Actions
{
    public class SaveCustomer: IAction
    {
        public SaveCustomer(Customer customer)
        {
            Customer = customer;
        }
        public Customer Customer { get; private set; }
    }

    
    
    public class SaveCustomerEffect : ActionEffect<SaveCustomer, CustomerState>
    {
        private readonly NorthwindContext _dbCtx;
        private readonly ILogger<SaveCustomerEffect> _logger;

        public SaveCustomerEffect(NorthwindContext dbCtx, ILogger<SaveCustomerEffect> logger)
        {
            _dbCtx = dbCtx;
            _logger = logger;
        }

        public override async Task<IAction> Effect(CustomerState prevState, SaveCustomer action)
        {
            _logger.LogInformation("Save Customer Effect");
            var customerEntity = (await _dbCtx.Set<Northwind.Domain.Customers.Customer>()
                                     .FirstAsync(c => c.CustomerId == action.Customer.CustomerId))
                                 ?? new Northwind.Domain.Customers.Customer();

            customerEntity.CompanyName = action.Customer.CompanyName;
            customerEntity.Address = action.Customer.Address;
            customerEntity.City = action.Customer.City;
            customerEntity.ContactName = action.Customer.ContactName;
            
            if (customerEntity.CustomerId == null)
            {
                _dbCtx.Set<Northwind.Domain.Customers.Customer>().Add(customerEntity);               
            }

            await _dbCtx.SaveChangesAsync();
            
            // reload customer
            var customer = await _dbCtx.Customers
                .Select(c => new Customer()
                {
                    Address = c.Address,
                    City = c.City,
                    CompanyName = c.CompanyName,
                    ContactName = c.ContactName,
                    CustomerId = c.CustomerId
                })
                .FirstOrDefaultAsync(c => c.CustomerId == customerEntity.CustomerId);

            return new SaveCustomerSuccessful(customer);
        }
    }
    
    
}