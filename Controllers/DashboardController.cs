using Microsoft.AspNetCore.Mvc;
using WebApplicationForEnterprise.Data;

namespace WebApplicationForEnterprise.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ProductsCount = _context.Products.Count();

            ViewBag.SuppliersCount = _context.Suppliers.Count();

            ViewBag.PurchaseOrdersCount = _context.PurchaseOrders.Count();

            ViewBag.LowStockProducts = _context.Products
                .Where(p => p.QuantityInStock < 5)
                .ToList();

            return View();
        }
    }
}