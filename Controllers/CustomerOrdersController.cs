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
    public class CustomerOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerOrders.Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmIssued(int id)
        {
            var customerOrder = await _context.CustomerOrders
                .Include(c => c.CustomerOrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            // щоб не можна було видати двічі
            if (customerOrder.Status == "Видано")
            {
                return RedirectToAction(
                    nameof(Details),
                    new { id });
            }

            var insufficientProducts = new List<string>();

            // спочатку лише перевірка
            foreach (var item in customerOrder.CustomerOrderItems)
            {
                if (item.Product != null)
                {
                    if (item.Product.QuantityInStock < item.Quantity)
                    {
                        insufficientProducts.Add(
                            $"{item.Product.Name} " +
                            $"(на складі: {item.Product.QuantityInStock}, потрібно: {item.Quantity})");
                    }
                }
            }

            // якщо є проблеми — нічого не списуємо
            if (insufficientProducts.Any())
            {
                TempData["Error"] =
                    "Недостатньо товарів:<br><br>" +
                    string.Join("<br>", insufficientProducts);

                return RedirectToAction(
                    nameof(Details),
                    new { id });
            }

            // якщо все ок — списуємо зі складу
            foreach (var item in customerOrder.CustomerOrderItems)
            {
                if (item.Product != null)
                {
                    item.Product.QuantityInStock -= item.Quantity;
                }
            }

            customerOrder.Status = "Видано";

            await _context.SaveChangesAsync();

            return RedirectToAction(
                nameof(Details),
                new { id });
        }

        // GET: CustomerOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrders
                .Include(c => c.Customer)
                .Include(c => c.CustomerOrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // GET: CustomerOrders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }

        // POST: CustomerOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,CustomerId")] CustomerOrder customerOrder)
        {
            if (ModelState.IsValid)
            {
                customerOrder.Status = "Оформлення";
                _context.Add(customerOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerOrder.CustomerId);
            return View(customerOrder);
        }

        // GET: CustomerOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrders.FindAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerOrder.CustomerId);
            return View(customerOrder);
        }

        // POST: CustomerOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDate,Status,CustomerId")] CustomerOrder customerOrder)
        {
            if (id != customerOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerOrderExists(customerOrder.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", customerOrder.CustomerId);
            return View(customerOrder);
        }

        // GET: CustomerOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrder = await _context.CustomerOrders
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            return View(customerOrder);
        }

        // POST: CustomerOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerOrder = await _context.CustomerOrders.FindAsync(id);
            if (customerOrder != null)
            {
                _context.CustomerOrders.Remove(customerOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerOrderExists(int id)
        {
            return _context.CustomerOrders.Any(e => e.Id == id);
        }
    }
}
