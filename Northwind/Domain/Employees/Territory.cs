using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Domain.Employees
{
    public partial class Territory
    {
        public Territory()
        {
        }

        [Column("TerritoryID")]
        [MaxLength(20)]
        public string TerritoryId { get; set; }

        [Column("RegionID")]
        public int RegionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TerritoryDescription { get; set; }

        [InverseProperty("Territory")]
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; } = new HashSet<EmployeeTerritory>();

        [InverseProperty("Territories")]
        public virtual Region Region { get; set; }
    }
}
