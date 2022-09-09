using SistemaFacturacion.Controllers;
using System;
using System.Collections.Generic;

namespace SistemaFacturacion.Models
{
    public partial class CustomerInvoice
    {
        public CustomerInvoice()
        {
            ProductInvoices = new HashSet<ProductInvoice>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Discount { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? AlreadyPaid { get; set; }
        public int TaxId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Tax Tax { get; set; } = null!;
        public virtual ICollection<ProductInvoice> ProductInvoices { get; set; }
    }
}
