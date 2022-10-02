using CachacasCanuto.Core.Entities.Products;

namespace CachacasCanuto.Application.ViewModels.Reports
{
    public class ReportItemViewModel
    {
        public Product Product { get; set; } = null!;
        public int QuantitySold { get; set; }
        public decimal TotalSold { get; set; }
    }
}
