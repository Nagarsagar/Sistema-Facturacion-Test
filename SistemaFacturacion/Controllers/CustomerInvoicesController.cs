using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Models;

namespace SistemaFacturacion.Controllers
{
    public class CustomerInvoicesController : Controller
    {
        private readonly InvoiceSystemContext _context;

        public CustomerInvoicesController(InvoiceSystemContext context)
        {
            _context = context;
        }

        // GET: CustomerInvoices
        public async Task<IActionResult> Index()
        {
            var invoiceSystemContext = _context.CustomerInvoices.Include(c => c.Customer).Include(c => c.Tax);
            return View(await invoiceSystemContext.ToListAsync());
        }

        // GET: CustomerInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerInvoices == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoices
                .Include(c => c.Customer)
                .Include(c => c.Tax)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        // GET: CustomerInvoices/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["TaxId"] = new SelectList(_context.Taxes, "Id", "Id");
            return View();
        }

        // POST: CustomerInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,SubTotal,TaxAmount,ShippingCost,Discount,GrandTotal,AlreadyPaid,TaxId")] CustomerInvoice customerInvoice)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(customerInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", customerInvoice.CustomerId);
            ViewData["TaxId"] = new SelectList(_context.Taxes, "Id", "Id", customerInvoice.TaxId);
            return View(customerInvoice);
        }

        // GET: CustomerInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerInvoices == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoices.FindAsync(id);
            if (customerInvoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", customerInvoice.CustomerId);
            ViewData["TaxId"] = new SelectList(_context.Taxes, "Id", "Id", customerInvoice.TaxId);
            return View(customerInvoice);
        }

        // POST: CustomerInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,SubTotal,TaxAmount,ShippingCost,Discount,GrandTotal,AlreadyPaid,TaxId")] CustomerInvoice customerInvoice)
        {
            if (id != customerInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerInvoiceExists(customerInvoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", customerInvoice.CustomerId);
            ViewData["TaxId"] = new SelectList(_context.Taxes, "Id", "Id", customerInvoice.TaxId);
            return View(customerInvoice);
        }

        // GET: CustomerInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerInvoices == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoices
                .Include(c => c.Customer)
                .Include(c => c.Tax)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        // POST: CustomerInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerInvoices == null)
            {
                return Problem("Entity set 'InvoiceSystemContext.CustomerInvoices'  is null.");
            }
            var customerInvoice = await _context.CustomerInvoices.FindAsync(id);
            if (customerInvoice != null)
            {
                _context.CustomerInvoices.Remove(customerInvoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerInvoiceExists(int id)
        {
            return (_context.CustomerInvoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
