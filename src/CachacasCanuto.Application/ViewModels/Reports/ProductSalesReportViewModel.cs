namespace CachacasCanuto.Application.ViewModels.Reports
{
    public class ProductSalesReportViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int QuantitySold { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
