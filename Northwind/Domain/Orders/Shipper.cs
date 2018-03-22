using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Domain.Orders
{
    public partial class Shipper
    {
        public Shipper()
        {
        }

        [Column("ShipperID")]
        [Key]
        public int ShipperId { get; set; }

        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }

        [MaxLength(24)]
        public string Phone { get; set; }

        [InverseProperty("Shipper")]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
