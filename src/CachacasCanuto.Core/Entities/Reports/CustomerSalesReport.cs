namespace CachacasCanuto.Core.Entities.Reports
{
    public class CustomerSalesReport
    {
        public CustomerSalesReport(string customerId, string customerName, decimal totalSales, List<ReportItem>? reports)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            TotalSales = totalSales;
            Reports = reports;
        }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSales { get; set; }
        public List<ReportItem>? Reports { get; set; }
    }
}
