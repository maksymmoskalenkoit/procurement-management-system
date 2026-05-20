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
public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();

        var model = new List<UserViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            model.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                RoleName = roles.FirstOrDefault() ?? "Немає ролі"
            });
        }

        return View(model);
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

        // GET
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };

            ViewBag.Roles = new List<string>
            {
                "Admin",
                "Manager",
                "WarehouseWorker"
            };

            return View(model);
        }


        // POST
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, model.Role);

            return RedirectToAction(nameof(Index));
        }
        // GET
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                RoleName = roles.FirstOrDefault() ?? "Немає ролі"
            };

            return View(model);
        }


        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}