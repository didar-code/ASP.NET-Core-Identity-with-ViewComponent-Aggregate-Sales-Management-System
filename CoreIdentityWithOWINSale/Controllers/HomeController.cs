using SalesCoreProjectWithIdentityViewCom.Models;
using SalesCoreProjectWithIdentityViewCom.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SalesCoreProjectWithIdentityViewCom.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(AppDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeDashboardViewModel
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
                IsAdmin = User.IsInRole("Admin"),
                DisplayName = User.Identity?.Name ?? "Guest"
            };

            if (model.IsAuthenticated)
            {
                var sales = _context.Sales
                    .Include(s => s.PaymentMethod)
                    .Include(s => s.Properties)
                    .AsQueryable();

                model.TotalSales = await sales.CountAsync();
                model.TotalRevenue = await sales.Select(x => (decimal?)x.TotalPrice).SumAsync() ?? 0;
                model.PaidSales = await sales.CountAsync(x => x.IsPaid);
                model.UnpaidSales = await sales.CountAsync(x => !x.IsPaid);
                model.TotalProperties = await _context.Properties.CountAsync();
                model.TotalUsers = model.IsAdmin ? await _userManager.Users.CountAsync() : 0;
                model.RecentSales = await sales
                    .OrderByDescending(x => x.SaleDate)
                    .Take(5)
                    .ToListAsync();
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}