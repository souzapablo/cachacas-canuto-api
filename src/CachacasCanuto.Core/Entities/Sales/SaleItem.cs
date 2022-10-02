using CachacasCanuto.Core.Entities.Shared;

namespace CachacasCanuto.Core.Entities.Sales
{
    public class SaleItem : BaseEntity
    {
        public SaleItem(decimal unitPrice, int quantity)
        {
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public decimal GetTotalAmount() => UnitPrice * Quantity;
    }
}
