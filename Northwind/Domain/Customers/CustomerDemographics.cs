using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Domain.Customers
{
    public partial class CustomerDemographics
    {
        public CustomerDemographics()
        {
        }

        [Key]
        [Column("CustomerTypeID"), MaxLength(10)]
        public string CustomerTypeId { get; set; }

        public string CustomerDesc { get; set; }

        [InverseProperty("CustomerType")]
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; } = new HashSet<CustomerCustomerDemo>();
    }
}
