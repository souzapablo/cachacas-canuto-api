namespace CachacasCanuto.Application.ViewModels.Sales
{
    public class SaleItemViewModel
    {
        public string Id { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
