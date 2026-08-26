using System.Diagnostics;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void NewProduct_ShouldHaveDraftStatus()
        {
            var price = new Price(50, "USD");

            var product = new Product(
                Guid.NewGuid(),
                "Mephiston_miniature",
                price,
                "warhammer 40k"
                );

            Assert.Equal(ProductStatus.Draft, product.Status);
        }

        [Fact]
        public void Publish_ShouldChangeStatusToPublished()
        {
            var price = new Price(50, "USD");

            var product = new Product(
                Guid.NewGuid(),
                "Mephiston_miniature",
                price,
                "warhammer 40k"
                );

            product.Publish();

            Assert.Equal(ProductStatus.Published, product.Status);
        }

        [Fact]
        public void Archive_DraftProduct_ShouldThrowException()
        {
            var price = new Price(50, "USD");

            var product = new Product(
                Guid.NewGuid(),
                "Mephiston_miniature",
                price,
                "warhammer 40k"
                );

            Assert.Throws<InvalidOperationException>(() => product.Archived());
        }

        [Fact]
        public void Product_ShouldFollowCorrectLifecycle()
        {
            var price = new Price(50, "USD");

            var product = new Product(
                Guid.NewGuid(),
                "Mephiston_miniature",
                price,
                "warhammer 40k"
                );

            product.Publish();
            product.Archived();

            Assert.Equal(ProductStatus.Archived, product.Status);
        }
    }
}