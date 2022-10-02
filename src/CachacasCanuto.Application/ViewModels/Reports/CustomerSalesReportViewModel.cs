using CachacasCanuto.Core.Entities.Reports;

namespace CachacasCanuto.Application.ViewModels.Reports
{
    public class CustomerSalesReportViewModel
    {

        public string CustomerId { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public decimal TotalSales { get; set; }
        public List<ReportItem> Reports { get; set; } = new();
    }
}
