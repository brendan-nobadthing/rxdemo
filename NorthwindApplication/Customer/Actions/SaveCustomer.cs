using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public SaveCustomerEffect(NorthwindContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public override async Task<IAction> Effect(CustomerState prevState, SaveCustomer action)
        {
            var customerEntity = (await _dbCtx.Set<Northwind.Domain.Customers.Customer>()
                                     .FirstAsync(c => c.CustomerId == action.Customer.CustomerId))
                                 ?? new Northwind.Domain.Customers.Customer();

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