namespace CachacasCanuto.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string Classification { get; set; } = null!;
        public decimal AlcooholContent { get; set; }
        public string Region { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
