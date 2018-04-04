using System.Collections.Generic;

namespace NorthwindApplication.Customer
{
    public class CustomerState 
    {
        public IList<OpenedCustomer> OpenCustomers { get; set; } = new List<OpenedCustomer>();                                
    }


    public class OpenedCustomer
    {
        public Customer Customer { get; set; }

        public IList<string> Users { get; set; } = new List<string>();

        public bool IsChanged { get; set; } = false;
    }
    
}