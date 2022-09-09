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

    public class ProductInvoicesController : Controller
    {
        private readonly InvoiceSystemContext _context;

        public ProductInvoicesController(InvoiceSystemContext context)
        {
            _context = context;
        }

        // GET: ProductInvoices
        public async Task<IActionResult> Index()
        {
            var invoiceSystemContext = _context.ProductInvoices.Include(p => p.CustomerInvoice).Include(p => p.Product);
            return View(await invoiceSystemContext.ToListAsync());
        }

        // GET: ProductInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductInvoices == null)
            {
                return NotFound();
            }

            var productInvoice = await _context.ProductInvoices
                .Include(p => p.CustomerInvoice)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInvoice == null)
            {
                return NotFound();
            }

            return View(productInvoice);
        }

        // GET: ProductInvoices/Create
        public IActionResult Create()
        {
            ViewData["CustomerInvoiceId"] = new SelectList(_context.CustomerInvoices, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: ProductInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerInvoiceId,ProductId,Amount,TotalPrice")] ProductInvoice productInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerInvoiceId"] = new SelectList(_context.CustomerInvoices, "Id", "Id", productInvoice.CustomerInvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productInvoice.ProductId);
            return View(productInvoice);
        }

        // GET: ProductInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductInvoices == null)
            {
                return NotFound();
            }

            var productInvoice = await _context.ProductInvoices.FindAsync(id);
            if (productInvoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerInvoiceId"] = new SelectList(_context.CustomerInvoices, "Id", "Id", productInvoice.CustomerInvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productInvoice.ProductId);
            return View(productInvoice);
        }

        // POST: ProductInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerInvoiceId,ProductId,Amount,TotalPrice")] ProductInvoice productInvoice)
        {
            if (id != productInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInvoiceExists(productInvoice.Id))
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
            ViewData["CustomerInvoiceId"] = new SelectList(_context.CustomerInvoices, "Id", "Id", productInvoice.CustomerInvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productInvoice.ProductId);
            return View(productInvoice);
        }

        // GET: ProductInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductInvoices == null)
            {
                return NotFound();
            }

            var productInvoice = await _context.ProductInvoices
                .Include(p => p.CustomerInvoice)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInvoice == null)
            {
                return NotFound();
            }

            return View(productInvoice);
        }

        // POST: ProductInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductInvoices == null)
            {
                return Problem("Entity set 'InvoiceSystemContext.ProductInvoices'  is null.");
            }
            var productInvoice = await _context.ProductInvoices.FindAsync(id);
            if (productInvoice != null)
            {
                _context.ProductInvoices.Remove(productInvoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInvoiceExists(int id)
        {
            return (_context.ProductInvoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
