using System;
using System.Collections.Generic;

namespace SistemaFacturacion.Models
{
    public partial class Tax
    {
        public Tax()
        {
            CustomerInvoices = new HashSet<CustomerInvoice>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Rate { get; set; }

        public virtual ICollection<CustomerInvoice> CustomerInvoices { get; set; }
    }
}
