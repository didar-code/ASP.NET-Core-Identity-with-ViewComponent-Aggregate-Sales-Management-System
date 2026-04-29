using SalesCoreProjectWithIdentityViewCom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SalesCoreProjectWithIdentityViewCom.ViewModels
{
    public class SalesViewModel
    {
        public int SalesId { get; set; }

        [Required(ErrorMessage = "Sale Date is required")]
        [Display(Name = "Sale Date")]
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Total Price is required")]
        [Range(1, 100000000, ErrorMessage = "Price must be between 1 and 100000000")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Client Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Client Name must be between 3 and 50 characters")]
        [Display(Name = "Client Name")]
        public string ClientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required")]
        [Display(Name = "Mobile No")]
        [RegularExpression(@"^01[3-9]\d{8}$", ErrorMessage = "Enter valid BD number")]
        [Remote(action: "IsMobileAvailable", controller: "Sales", AdditionalFields = nameof(SalesId))]
        public string MobileNo { get; set; } = string.Empty;

        public string? ClientImage { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Payment Type is required")]
        public int PaymentTypeId { get; set; }

        public bool IsPaid { get; set; }

        public IFormFile? ProfileFile { get; set; }

        public string? PaymentTypeName { get; set; }

        public IList<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

        public IList<PropertyViewModel> Properties { get; set; } = new List<PropertyViewModel>();
    }

    public class PropertyViewModel
    {
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "Property Type is required")]
        [StringLength(50, ErrorMessage = "Property Type cannot exceed 50 characters")]
        public string PropertyType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Location { get; set; } = string.Empty;

        public int SalesId { get; set; }
    }
}