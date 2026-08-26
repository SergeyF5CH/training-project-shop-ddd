using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Domain.Tests
{
    public class PriceTests
    {
        [Fact]
        public void CreatePrice_WithPositiveAmount_ShouldSucceed()
        {
            var price = new Price(100, "USD");

            Assert.Equal(100, price.Amount);
            Assert.Equal("USD", price.Currency);
        }

        [Fact]
        public void CreatePrice_WithNegativeAmount_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => new Price(-1, "USD"));
        }

        [Fact]
        public void CreatePrice_WithEmptyCurrency_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => new Price(100, ""));
        }

        [Fact]
        public void TwoPrices_WithSameValues_ShouldBeEqual()
        {
            var price1 = new Price(100, "USD");
            var price2 = new Price(100, "USD");

            Assert.Equal(price1, price2);
        }
    }
}
