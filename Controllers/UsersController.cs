using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationForEnterprise.ViewModels;

namespace WebApplicationForEnterprise.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // список користувачів
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();

            return View(users);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager
                    .CreateAsync(
                        user,
                        model.Password);

                if (result.Succeeded)
                {
                    await _userManager
                        .AddToRoleAsync(
                            user,
                            model.Role);

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        "",
                        error.Description);
                }
            }

            return View(model);
        }
    }
}