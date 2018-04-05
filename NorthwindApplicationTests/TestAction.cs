using NorthwindApplication.Customer;
using NorthwindApplication.Customer.Actions;
using Xunit;

namespace NorthwindApplicationTests
{
    public class TestAction
    {

        [Fact]
        public void HelloXunit()
        {
            Assert.True(true);
        }
        
        
//        [Fact]
//        public void OpenCustomerAction()
//        {
//            var customerManager = new CustomerManager();
//
//            customerManager.CustomerStore.Dispatch(new OpenCustomer(
//                new Customer()
//                {
//                    CustomerId = "12345",
//                    CompanyName = "SSW"
//                }, 
//                "TestUserAccount"));
//
//            var state = customerManager.CustomerStore.GetState();
//            Assert.NotEmpty(state.OpenCustomers);
//        }
        
    }
}