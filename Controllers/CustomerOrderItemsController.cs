using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationForEnterprise.Data;
using WebApplicationForEnterprise.Models;

namespace WebApplicationForEnterprise.Controllers
{
    public class CustomerOrderItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerOrderItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerOrderItems.Include(c => c.CustomerOrder).Include(c => c.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerOrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrderItem = await _context.CustomerOrderItems
                .Include(c => c.CustomerOrder)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerOrderItem == null)
            {
                return NotFound();
            }

            return View(customerOrderItem);
        }
        // GET: CustomerOrderItems/Create
        public IActionResult Create(int? customerOrderId)
        {
            ViewBag.CustomerOrderId =
                customerOrderId;

            ViewBag.ProductIdData =
                _context.Products.ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerOrderId,ProductId,Quantity,Price")] CustomerOrderItem customerOrderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerOrderItem);

                await _context.SaveChangesAsync();

                return RedirectToAction(
                    "Details",
                    "CustomerOrders",
                    new { id = customerOrderItem.CustomerOrderId });
            }

            ViewBag.CustomerOrderId =
                customerOrderItem.CustomerOrderId;

            ViewBag.ProductIdData =
                _context.Products.ToList();

            return View(customerOrderItem);
        }

        // GET: CustomerOrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrderItem =
                await _context.CustomerOrderItems.FindAsync(id);

            if (customerOrderItem == null)
            {
                return NotFound();
            }

            ViewBag.ProductIdData =
                _context.Products.ToList();

            return View(customerOrderItem);
        }


        // POST: CustomerOrderItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,CustomerOrderId,ProductId,Quantity,Price")]
    CustomerOrderItem customerOrderItem)
        {
            if (id != customerOrderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerOrderItem);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerOrderItemExists(customerOrderItem.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(
                    "Details",
                    "CustomerOrders",
                    new { id = customerOrderItem.CustomerOrderId });
            }

            ViewBag.ProductIdData =
                _context.Products.ToList();

            return View(customerOrderItem);
        }

        // GET: CustomerOrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrderItem = await _context.CustomerOrderItems
                .Include(c => c.CustomerOrder)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerOrderItem == null)
            {
                return NotFound();
            }

            return View(customerOrderItem);
        }

        // POST: CustomerOrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerOrderItem = await _context.CustomerOrderItems.FindAsync(id);
            if (customerOrderItem != null)
            {
                _context.CustomerOrderItems.Remove(customerOrderItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuickDelete(int id)
        {
            var customerOrderItem =
                await _context.CustomerOrderItems.FindAsync(id);

            if (customerOrderItem == null)
            {
                return NotFound();
            }

            var customerOrderId =
                customerOrderItem.CustomerOrderId;

            _context.CustomerOrderItems.Remove(
                customerOrderItem);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "CustomerOrders",
                new { id = customerOrderId });
        }
        private bool CustomerOrderItemExists(int id)
        {
            return _context.CustomerOrderItems.Any(e => e.Id == id);
        }
    }
}
