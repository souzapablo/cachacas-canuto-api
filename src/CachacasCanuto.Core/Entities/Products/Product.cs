using CachacasCanuto.Core.Entities.Shared;

namespace CachacasCanuto.Core.Entities.Products
{
    public class Product : BaseEntity
    {
        public Product(string name, string label, string classification, decimal alcooholContent, string region, decimal price)
        {
            Name = name;
            Label = label;
            Classification = classification;
            AlcooholContent = alcooholContent;
            Region = region;
            Price = price;
        }

        public string Name { get; private set; }
        public string Label { get; private set; }
        public string Classification { get; private set; }
        public decimal AlcooholContent { get; private set; }
        public string Region { get; private set; }
        public decimal Price { get; private set; }
    }
}
