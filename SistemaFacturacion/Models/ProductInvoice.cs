using System;
using System.Collections.Generic;

namespace SistemaFacturacion.Models
{
    public partial class ProductInvoice
    {
        public int Id { get; set; }
        public int CustomerInvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual CustomerInvoice CustomerInvoice { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
