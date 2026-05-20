using Microsoft.AspNetCore.Mvc;

namespace WebApplicationForEnterprise.Controllers
{
    public class ClientModuleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
