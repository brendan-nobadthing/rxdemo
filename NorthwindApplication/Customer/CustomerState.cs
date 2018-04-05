using System.Collections.Generic;

namespace NorthwindApplication.Customer
{
    public class CustomerState 
    {
        /// <summary>
        /// Customers that have been opened for editing
        /// </summary>
        public IList<OpenedCustomer> OpenCustomers { get; set; } = new List<OpenedCustomer>();

        /// <summary>
        /// list of all saved customers
        /// </summary>
        public IList<Customer> CustomerList { get; set; } = new List<Customer>();
    }


    public class OpenedCustomer
    {
        public Customer Customer { get; set; }

        public IList<string> Users { get; set; } = new List<string>();

        public bool IsChanged { get; set; } = false;
    }
    
}