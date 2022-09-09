using System;
using System.Collections.Generic;

namespace SistemaFacturacion.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerInvoices = new HashSet<CustomerInvoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }

        public virtual ICollection<CustomerInvoice> CustomerInvoices { get; set; }
    }
}
