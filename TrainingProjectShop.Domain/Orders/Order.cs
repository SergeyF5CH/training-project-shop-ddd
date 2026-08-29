using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Domain.Orders
{
    public class Order
    {
        private readonly List<OrderItem> _items = new();

        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Order(Guid id, Guid customerId)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Order Id can't be empty", nameof(id));
            }

            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("CustomerId can't be empty", nameof(customerId));
            }

            Id = id;
            CustomerId = customerId;
            Status = OrderStatus.Created;
        }

        private Order() { }

        public void AddItem(Guid productId,
            Price price,
            int quantity)
        {
            if(Status != OrderStatus.Created)
            {
                throw new InvalidOperationException("Items can only be added to a Created order");
            }

            var item = new OrderItem(productId, price, quantity);

            _items.Add(item);
        }

        public void Confirm()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Order must containt > 0 item");
            }

            if(Status != OrderStatus.Created)
            {
                throw new InvalidOperationException("Only Created orders can be confirmed");
            }

            Status = OrderStatus.Confirmed;
        }

        public void Pay()
        {
            if (Status != OrderStatus.Confirmed)
            {
                throw new InvalidOperationException("Only Confirmed orders can be paid");
            }

            Status = OrderStatus.Paid;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Paid)
            {
                throw new InvalidOperationException("Paid order can't be cancel");
            }

            if (Status == OrderStatus.Canceled)
            {
                throw new InvalidOperationException("Canceled order can't be cancel");
            }

            Status = OrderStatus.Canceled;
        }
    }
}
