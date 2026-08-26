using TrainingProjectShop.Domain.Customers;

namespace TrainingProjectShop.Domain.Tests;

public class EmailTests
{
    [Fact]
    public void CreateEmail_WithValidValue_ShouldSucceed()
    {
        var email = new Email("user@example.com");

        Assert.Equal("user@example.com", email.Value);
    }

    [Fact]
    public void CreateEmail_WithEmptyValue_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => new Email(""));
    }

    [Fact]
    public void CreateEmail_WithoutAtSymbol_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => new Email("userExample.com"));
    }

    [Fact]
    public void TwoEmails_WithSameValue_ShouldBeEqual()
    {
        var email1 = new Email("user@example.com");
        var email2 = new Email("user@example.com");

        Assert.Equal(email1, email2);
    }
}
