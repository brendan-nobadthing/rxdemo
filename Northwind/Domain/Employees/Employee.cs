using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Employees
{
    public partial class Employee
    {
        public Employee()
        {
        }

        [Column("EmployeeID")]
        public int EmployeeId { get; set; }

        [MaxLength(60)]
        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(15)]
        public string City { get; set; }

        [MaxLength(15)]
        public string Country { get; set; }

        [MaxLength(4)]
        public string Extension { get; set; }

        [Required]
        [MaxLength(10)]
        public string FirstName { get; set; }

        public DateTime? HireDate { get; set; }

        [MaxLength(24)]
        public string HomePhone { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string Notes { get; set; }

        public byte[] Photo { get; set; }

        [MaxLength(255)]
        public string PhotoPath { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(15)]
        public string Region { get; set; }

        public int? ReportsTo { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }

        [MaxLength(25)]
        public string TitleOfCourtesy { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<EmployeeTerritory> EmployeeTerritories { get; set; } = new HashSet<EmployeeTerritory>();

        [InverseProperty("Employee")]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        [ForeignKey("ReportsTo")]
        [InverseProperty("DirectReports")]
        public virtual Employee Manager { get; set; }

        [InverseProperty("Manager")]
        public virtual ICollection<Employee> DirectReports { get; set; }
    }
}
