using SalesCoreProjectWithIdentityViewCom.Models;
using SalesCoreProjectWithIdentityViewCom.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SalesCoreProjectWithIdentityViewCom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDBContext _context;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDBContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PaymentMethods = await _context.PaymentMethods
                .OrderBy(x => x.PaymentType)
                .ToListAsync();

            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ManageUserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName ?? user.Email ?? "User",
                Roles = new List<RoleSelection>()
            };

            foreach (var role in await _roleManager.Roles.ToListAsync())
            {
                model.Roles.Add(new RoleSelection
                {
                    RoleName = role.Name ?? string.Empty,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name ?? string.Empty)
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(ManageUserRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.Roles
                .Where(r => r.IsSelected)
                .Select(r => r.RoleName)
                .ToList();

            var resultRemove = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            if (!resultRemove.Succeeded)
            {
                foreach (var error in resultRemove.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                ViewBag.PaymentMethods = await _context.PaymentMethods
                    .OrderBy(x => x.PaymentType)
                    .ToListAsync();

                return View(model);
            }

            var resultAdd = await _userManager.AddToRolesAsync(user, selectedRoles);
            if (!resultAdd.Succeeded)
            {
                foreach (var error in resultAdd.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                ViewBag.PaymentMethods = await _context.PaymentMethods
                    .OrderBy(x => x.PaymentType)
                    .ToListAsync();

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePaymentMethod(string paymentType)
        {
            var normalizedName = paymentType?.Trim();

            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                TempData["PaymentMethodError"] = "Payment method name is required.";
                return RedirectToAction(nameof(Index));
            }

            var exists = await _context.PaymentMethods
                .AnyAsync(x => x.PaymentType.ToLower() == normalizedName.ToLower());

            if (exists)
            {
                TempData["PaymentMethodError"] = "This payment method already exists.";
                return RedirectToAction(nameof(Index));
            }

            var method = new PaymentMethod
            {
                PaymentType = normalizedName
            };

            _context.PaymentMethods.Add(method);
            await _context.SaveChangesAsync();

            TempData["PaymentMethodSuccess"] = "Payment method added successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}