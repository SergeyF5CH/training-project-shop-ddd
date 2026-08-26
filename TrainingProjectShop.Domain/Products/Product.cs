namespace TrainingProjectShop.Domain.Products
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public string? Description { get; private set; }
        public ProductStatus Status { get; private set; }

        public Product(Guid id, 
            string name, 
            Price price, 
            string? description)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Product id can't be empty",nameof(id));
            }

            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name can't be empty", nameof(name));
            }

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

        public void ChangePrice(Price price)
        {
            if (Status == ProductStatus.Archived)
            {
                throw new InvalidOperationException("Archived products can't change price");
            }

            Price = price;
        }
    }
}
