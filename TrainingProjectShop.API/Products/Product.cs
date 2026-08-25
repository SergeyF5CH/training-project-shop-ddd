namespace TrainingProjectShop.API.Products
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string? Description { get; private set; }
        public ProductStatus Status { get; private set; }

        public Product(Guid id, 
            string name, 
            decimal price, 
            string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Status = ProductStatus.Draft;
        }

        public void Publish()
        {
            if (Status != ProductStatus.Draft)
            {
                throw new InvalidOperationException("Only Draft product can be published");
            }
            Status = ProductStatus.Published;
        }

        public void Archived()
        {
            if (Status != ProductStatus.Published)
            {
                throw new InvalidOperationException("Only Published product can be Archived");
            }
            Status = ProductStatus.Archived;
        }

        public void ChangePrice(decimal price)
        {
            if (Status == ProductStatus.Archived)
            {
                throw new InvalidOperationException("Archived products can't change price");
            }

            if(price <= 0)
            {
                throw new ArgumentException("Price must be greatet than 0");
            }

            Price = price;
        }
    }
}
