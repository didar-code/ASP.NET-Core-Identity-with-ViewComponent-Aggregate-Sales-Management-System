using SalesCoreProjectWithIdentityViewCom.Models;
using SalesCoreProjectWithIdentityViewCom.Models.ViewModels;
using SalesCoreProjectWithIdentityViewCom.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SalesCoreProjectWithIdentityViewCom.ViewComponents
{
    public class SalesSummaryViewComponent : ViewComponent
    {
        private readonly AppDBContext _context;

        public SalesSummaryViewComponent(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sales = _context.Sales
                .Include(x => x.Properties)
                .AsQueryable();

            var model = new SalesSummaryViewModel
            {
                TotalSales = await sales.CountAsync(),
                TotalRevenue = await sales.Select(x => (decimal?)x.TotalPrice).SumAsync() ?? 0,
                PaidSales = await sales.CountAsync(x => x.IsPaid),
                UnpaidSales = await sales.CountAsync(x => !x.IsPaid),
                PaidRevenue = await sales.Where(x => x.IsPaid).Select(x => (decimal?)x.TotalPrice).SumAsync() ?? 0,
                UnpaidRevenue = await sales.Where(x => !x.IsPaid).Select(x => (decimal?)x.TotalPrice).SumAsync() ?? 0,
                TotalProperties = await _context.Properties.CountAsync(),
                AverageSaleAmount = await sales.AnyAsync()
                    ? await sales.AverageAsync(x => x.TotalPrice)
                    : 0
            };

            return View(model);
        }
    }
}