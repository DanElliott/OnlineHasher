using Bunit;
using IndexPage = OnlineHasher.Pages.Index;

namespace OnlineHasher.Tests;

public class IndexPageTests : TestContext
{
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
    public void Submit_WithPasswordAndSalt_ShowsHash()
    {
        var cut = Submit("password", "salt");

        Assert.Equal(
            "6E88BE8BAD7EAE9D9E10AA061224034FED48D03FCBAD968B56006784539D5214",
            cut.Find("#hash").TextContent);
    }

    [Fact]
    public void Submit_WithoutPassword_ShowsErrorAndNoHash()
    {
        var cut = Submit("", "salt");

        Assert.Empty(cut.FindAll("#hash"));
        Assert.Equal("Password is required.", cut.Find("#error").TextContent);
    }
}

