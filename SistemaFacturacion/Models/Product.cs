using System;
using System.Collections.Generic;

namespace SistemaFacturacion.Models
{
    public partial class Product 
    {
        public Product()
        {
            ProductInvoices = new HashSet<ProductInvoice>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<ProductInvoice> ProductInvoices { get; set; }
    }
}
