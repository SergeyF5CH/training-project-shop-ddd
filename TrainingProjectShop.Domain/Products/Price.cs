using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingProjectShop.Domain.Products
{
    public sealed record Price
    {
        public decimal Amount { get; }
        public string Currency {  get; }

        public Price(decimal amount, string currency)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Product price must be greatet than 0", nameof(amount));
            }

            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("Currency can't be empty", nameof(currency));
            }

            Amount = amount;
            Currency = currency;
        }
    }
}
