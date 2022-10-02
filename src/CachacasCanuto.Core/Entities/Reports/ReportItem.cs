using CachacasCanuto.Core.Entities.Products;

namespace CachacasCanuto.Core.Entities.Reports
{
    public class ReportItem
    {
        public ReportItem(Product product, int quantitySold, decimal totalSold)
        {
            Product = product;
            QuantitySold = quantitySold;
            TotalSold = totalSold;
        }

        public Product Product { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalSold { get; set; }
    }
}
