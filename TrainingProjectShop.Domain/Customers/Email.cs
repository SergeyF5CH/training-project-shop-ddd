namespace TrainingProjectShop.Domain.Customers
{
    public sealed record Email
    {
        public string Value { get; }
        public Email(string value) 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email can't be empty", nameof(value));
            }

            if (!value.Contains("@"))
            {
                throw new ArgumentException("Invalid Email format", nameof(value));
            }

            Value = value;
        }
    }
}
