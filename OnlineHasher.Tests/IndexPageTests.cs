using System.Security.Cryptography;
using System.Text;
using Bunit;
using IndexPage = OnlineHasher.Pages.Index;

namespace OnlineHasher.Tests;

public class IndexPageTests : TestContext
{
    private static string Expected(string password, string salt) =>
        Convert.ToHexString(Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), 1000, HashAlgorithmName.SHA1, 32));

    private IRenderedComponent<IndexPage> Submit(string password, string salt)
    {
        var cut = RenderComponent<IndexPage>();
        cut.Find("#password").Change(password);
        cut.Find("#salt").Change(salt);
        cut.Find("form").Submit();
        return cut;
    }

    [Fact]
    public void InitialRender_ShowsFormAndNoHash()
    {
        var cut = RenderComponent<IndexPage>();

        Assert.NotNull(cut.Find("#password"));
        Assert.NotNull(cut.Find("#salt"));
        Assert.Empty(cut.FindAll("#hash"));
    }

    [Fact]
    public void Submit_WithPasswordAndSalt_ShowsPbkdf2Hash()
    {
        var cut = Submit("password", "salt");

        Assert.Equal(Expected("password", "salt"), cut.Find("#hash").TextContent);
    }

    [Fact]
    public void Submit_WithoutSalt_UsesEmptySalt()
    {
        var cut = Submit("password", "");

        Assert.Equal(Expected("password", ""), cut.Find("#hash").TextContent);
    }

    [Fact]
    public void Submit_WithWhitespaceSalt_UsesEmptySalt()
    {
        var cut = Submit("password", "   ");

        Assert.Equal(Expected("password", ""), cut.Find("#hash").TextContent);
    }

    [Fact]
    public void Submit_IsDeterministicAndSaltSensitive()
    {
        var a = Submit("pw", "s1").Find("#hash").TextContent;
        var b = Submit("pw", "s1").Find("#hash").TextContent;
        var c = Submit("pw", "s2").Find("#hash").TextContent;

        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.Equal(64, a.Length);
    }

    [Fact]
    public void Submit_WithoutPassword_FailsValidationAndShowsNoHash()
    {
        var cut = Submit("", "salt");

        Assert.Empty(cut.FindAll("#hash"));
    }
}

