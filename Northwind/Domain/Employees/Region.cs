using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Domain.Employees
{
    public partial class Region
    {
        public Region()
        {
        }
       

        [Column("RegionID")]
        public int RegionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RegionDescription { get; set; }

        [InverseProperty("Region")]
        public virtual ICollection<Territory> Territories { get; set; } = new HashSet<Territory>();
    }
}
