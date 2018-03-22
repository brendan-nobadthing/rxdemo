using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Domain.Products
{
    public partial class Category
    {
        public Category()
        {
        }

        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(15)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
