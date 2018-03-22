﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.Domain.Orders;

namespace Northwind.Domain.Customers
{
    public partial class Customer
    {
        public Customer()
        {
        }

        [Column("CustomerID")]
        [MaxLength(5)]
        public string CustomerId { get; set; }

        [MaxLength(60)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string City { get; set; }

        [Required]
        [MaxLength(40)]
        public string CompanyName { get; set; }

        [MaxLength(30)]
        public string ContactName { get; set; }

        [MaxLength(30)]
        public string ContactTitle { get; set; }

        [MaxLength(15)]
        public string Country { get; set; }

        [MaxLength(24)]
        public string Fax { get; set; }

        [MaxLength(24)]
        public string Phone { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(15)]
        public string Region { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; } = new HashSet<CustomerCustomerDemo>();

        [InverseProperty("Customer")]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
