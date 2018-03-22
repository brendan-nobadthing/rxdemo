﻿using System.ComponentModel.DataAnnotations.Schema;
using Northwind.Domain.Products;

namespace Northwind.Domain.Orders
{
    [Table("Order Details")]
    public partial class OrderDetail
    {
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Column("ProductID")]
        public int ProductId { get; set; }

        public float Discount { get; set; }

        public short Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }

        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }

        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; }
    }
}
