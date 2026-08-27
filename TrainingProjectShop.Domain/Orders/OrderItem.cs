using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Domain.Orders
{
    public class OrderItem
    {
        public Guid ProductId { get; private set; }
        public Price Price { get; private set; }
        public int Quantity { get; private set; }
        public OrderItem(Guid productId,
            Price price,
            int quantity) 
        {
            if (productId == Guid.Empty) 
            {
                throw new ArgumentException("ProductId can't be empty", nameof(productId));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greatet than 0", nameof(quantity));
            }

            ProductId = productId;
            Price = price;
            Quantity = quantity;
        }

        public decimal GetTotal()
        {
            return Price.Amount * Quantity;
        }
    }
}
