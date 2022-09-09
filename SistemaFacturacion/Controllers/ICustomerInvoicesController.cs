using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.Models;

namespace SistemaFacturacion.Controllers
{
    public interface ICustomerInvoicesController
    {
        IActionResult Create();
        Task<IActionResult> Create([Bind(new[] { "Id,CustomerId,SubTotal,TaxAmount,ShippingCost,Discount,GrandTotal,AlreadyPaid,TaxId" })] CustomerInvoice customerInvoice);
        Task<IActionResult> Delete(int? id);
        Task<IActionResult> DeleteConfirmed(int id);
        Task<IActionResult> Details(int? id);
        Task<IActionResult> Edit(int id, [Bind(new[] { "Id,CustomerId,SubTotal,TaxAmount,ShippingCost,Discount,GrandTotal,AlreadyPaid,TaxId" })] CustomerInvoice customerInvoice);
        Task<IActionResult> Edit(int? id);
        Task<IActionResult> Index();
    }
}