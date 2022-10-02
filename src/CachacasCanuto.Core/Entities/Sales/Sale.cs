using CachacasCanuto.Core.Entities.Shared;

namespace CachacasCanuto.Core.Entities.Sales
{
    public class Sale : BaseEntity
    {
        public Sale(string customerId, DateTime date, List<SaleItem> itens)
        {
            CustomerId = customerId;
            Date = date;
            Itens = itens;
        }

        public string CustomerId { get; private set; } = null!;
        public DateTime Date { get; private set; }
        public List<SaleItem> Itens { get; private set; }

        public bool ItensContainId(List<string> ids)
        {
            foreach (var item in Itens)
            {
                if (ids.Contains(item.Id))
                    return true;
            }

            return false;
        }

        public decimal CalculatePrice()
        {
            var price = 0m;

            foreach (var item in Itens)
            {
                price += item.UnitPrice * item.Quantity;
            }

            return price;
        }
    }
}
