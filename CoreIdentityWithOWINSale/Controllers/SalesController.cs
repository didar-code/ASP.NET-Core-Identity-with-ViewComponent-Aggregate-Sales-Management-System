using SalesCoreProjectWithIdentityViewCom.Models;
using SalesCoreProjectWithIdentityViewCom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace SalesCoreProjectWithIdentityViewCom.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IWebHostEnvironment _env;

        public SalesController(AppDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(
            string? search,
            DateTime? fromDate,
            int? paymentTypeId,
            bool? isPaid,
            string? sortOrder,
            int? page)
        {
            var sales = _context.Sales
                .Include(s => s.Properties)
                .Include(s => s.PaymentMethod)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                sales = sales.Where(s =>
                    s.ClientName.Contains(search) ||
                    s.MobileNo.Contains(search));
            }

            if (fromDate.HasValue)
            {
                var date = fromDate.Value.Date;
                sales = sales.Where(s => s.SaleDate.Date >= date);
            }

            if (paymentTypeId.HasValue)
            {
                sales = sales.Where(s => s.PaymentTypeId == paymentTypeId.Value);
            }

            if (isPaid.HasValue)
            {
                sales = sales.Where(s => s.IsPaid == isPaid.Value);
            }

            ViewBag.CurrentSort = sortOrder;

            ViewBag.DateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewBag.PriceSort = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            ViewBag.ClientSort = sortOrder == "client_asc" ? "client_desc" : "client_asc";
            ViewBag.MobileSort = sortOrder == "mobile_asc" ? "mobile_desc" : "mobile_asc";
            ViewBag.PaymentSort = sortOrder == "payment_asc" ? "payment_desc" : "payment_asc";
            ViewBag.StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc";

            sales = sortOrder switch
            {
                "date_asc" => sales.OrderBy(s => s.SaleDate),
                "date_desc" => sales.OrderByDescending(s => s.SaleDate),

                "price_asc" => sales.OrderBy(s => s.TotalPrice),
                "price_desc" => sales.OrderByDescending(s => s.TotalPrice),

                "client_asc" => sales.OrderBy(s => s.ClientName),
                "client_desc" => sales.OrderByDescending(s => s.ClientName),

                "mobile_asc" => sales.OrderBy(s => s.MobileNo),
                "mobile_desc" => sales.OrderByDescending(s => s.MobileNo),

                "payment_asc" => sales.OrderBy(s => s.PaymentMethod.PaymentType),
                "payment_desc" => sales.OrderByDescending(s => s.PaymentMethod.PaymentType),

                "status_asc" => sales.OrderBy(s => s.IsPaid),
                "status_desc" => sales.OrderByDescending(s => s.IsPaid),

                _ => sales.OrderByDescending(s => s.SaleDate)
            };

            ViewBag.PaymentMethods = _context.PaymentMethods.ToList();
            ViewBag.CurrentSearch = search;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.PaymentTypeId = paymentTypeId;
            ViewBag.IsPaid = isPaid?.ToString().ToLower();

            return View(sales.ToPagedList(page ?? 1, 3));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePartial()
        {
            var model = new SalesViewModel
            {
                PaymentMethods = _context.PaymentMethods.ToList(),
                Properties = new List<PropertyViewModel>()
            };

            return PartialView("_CreateSalesPartial", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSale(SalesViewModel model)
        {
            ValidateImageFile(model.ProfileFile);

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(
                        k => k.Key,
                        v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { success = false, errors });
            }

            var imagePath = await SaveImage(model.ProfileFile);

            var sale = new Sale
            {
                ClientName = model.ClientName,
                MobileNo = model.MobileNo,
                TotalPrice = model.TotalPrice,
                SaleDate = model.SaleDate,
                IsPaid = model.IsPaid,
                PaymentTypeId = model.PaymentTypeId,
                ClientImage = imagePath,
                Properties = model.Properties?
                    .Where(p => !string.IsNullOrWhiteSpace(p.PropertyType) || !string.IsNullOrWhiteSpace(p.Location))
                    .Select(p => new Property
                    {
                        PropertyType = p.PropertyType,
                        Location = p.Location
                    }).ToList() ?? new List<Property>()
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPartial(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Properties)
                .Include(s => s.PaymentMethod)
                .FirstOrDefaultAsync(s => s.SalesId == id);

            if (sale == null)
            {
                return NotFound("Sale not found");
            }

            var vm = new SalesViewModel
            {
                SalesId = sale.SalesId,
                SaleDate = sale.SaleDate,
                TotalPrice = sale.TotalPrice,
                ClientName = sale.ClientName,
                MobileNo = sale.MobileNo,
                PaymentTypeId = sale.PaymentTypeId,
                IsPaid = sale.IsPaid,
                ClientImage = sale.ClientImage,
                Properties = sale.Properties.Select(p => new PropertyViewModel
                {
                    PropertyId = p.PropertyId,
                    SalesId = p.SalesId,
                    PropertyType = p.PropertyType,
                    Location = p.Location
                }).ToList(),
                PaymentMethods = await _context.PaymentMethods.ToListAsync()
            };

            if (vm.Properties == null || vm.Properties.Count == 0)
            {
                vm.Properties = new List<PropertyViewModel> { new PropertyViewModel() };
            }

            return PartialView("_EditSalesPartial", vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSale(SalesViewModel model, string? oldClientImage)
        {
            ValidateImageFile(model.ProfileFile);

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(
                        k => k.Key,
                        v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { success = false, errors });
            }

            var sale = await _context.Sales
                .Include(s => s.Properties)
                .FirstOrDefaultAsync(s => s.SalesId == model.SalesId);

            if (sale == null)
            {
                return Json(new
                {
                    success = false,
                    errors = new Dictionary<string, string[]>
                    {
                        { "SalesId", new[] { "Sale not found." } }
                    }
                });
            }

            sale.SaleDate = model.SaleDate;
            sale.TotalPrice = model.TotalPrice;
            sale.ClientName = model.ClientName;
            sale.MobileNo = model.MobileNo;
            sale.PaymentTypeId = model.PaymentTypeId;
            sale.IsPaid = model.IsPaid;

            if (model.ProfileFile != null && model.ProfileFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(sale.ClientImage) &&
                    !sale.ClientImage.EndsWith("noImg.jpg", StringComparison.OrdinalIgnoreCase) &&
                    !sale.ClientImage.EndsWith("noImg.png", StringComparison.OrdinalIgnoreCase))
                {
                    var oldImageRelativePath = sale.ClientImage.TrimStart('~', '/').Replace("/", Path.DirectorySeparatorChar.ToString());
                    var oldPath = Path.Combine(_env.WebRootPath, oldImageRelativePath);

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                sale.ClientImage = await SaveImage(model.ProfileFile);
            }
            else
            {
                sale.ClientImage = oldClientImage;
            }

            var incomingExistingIds = (model.Properties ?? new List<PropertyViewModel>())
                .Where(p => p != null && p.PropertyId > 0)
                .Select(p => p.PropertyId)
                .ToList();

            var toRemove = sale.Properties
                .Where(p => !incomingExistingIds.Contains(p.PropertyId))
                .ToList();

            if (toRemove.Any())
            {
                _context.Properties.RemoveRange(toRemove);
            }

            if (model.Properties != null)
            {
                foreach (var item in model.Properties)
                {
                    if (item == null) continue;

                    if (string.IsNullOrWhiteSpace(item.PropertyType) &&
                        string.IsNullOrWhiteSpace(item.Location))
                    {
                        continue;
                    }

                    if (item.PropertyId > 0)
                    {
                        var existing = sale.Properties.FirstOrDefault(p => p.PropertyId == item.PropertyId);
                        if (existing != null)
                        {
                            existing.PropertyType = item.PropertyType;
                            existing.Location = item.Location;
                        }
                    }
                    else
                    {
                        sale.Properties.Add(new Property
                        {
                            SalesId = sale.SalesId,
                            PropertyType = item.PropertyType,
                            Location = item.Location
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index")
            });
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsMobileAvailable(string mobileNo, int salesId)
        {
            var exists = await _context.Sales
                .AnyAsync(s => s.MobileNo == mobileNo && s.SalesId != salesId);

            return exists
                ? Json($"Mobile number {mobileNo} is already in use")
                : Json(true);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sale = await _context.Sales
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(x => x.SalesId == id);

            if (sale == null)
            {
                return Json(new { success = false, message = "Not found" });
            }

            _context.Properties.RemoveRange(sale.Properties);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        private void ValidateImageFile(IFormFile? file)
        {
            if (file == null) return;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(ext))
            {
                ModelState.AddModelError("ProfileFile", "Only .jpg, .jpeg, and .png files are allowed.");
            }

            if (file.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("ProfileFile", "Image size must be less than 2 MB.");
            }
        }

        private async Task<string> SaveImage(IFormFile? file)
        {
            if (file == null)
            {
                return "~/Images/noImg.jpg";
            }

            var folder = Path.Combine(_env.WebRootPath, "Images");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(folder, fileName);

            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return "~/Images/" + fileName;
        }
    }
}