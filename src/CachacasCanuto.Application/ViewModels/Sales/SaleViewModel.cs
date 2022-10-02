namespace CachacasCanuto.Application.ViewModels.Sales
{
    public class SaleViewModel
    {
        public string Id { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<SaleItemViewModel> Itens { get; set; } = null!;
    }
}
