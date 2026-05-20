using Microsoft.AspNetCore.Mvc;

namespace WebApplicationForEnterprise.Controllers
{
    public class WarehouseModuleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
