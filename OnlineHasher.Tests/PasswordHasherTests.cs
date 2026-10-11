using System.Security.Cryptography;
using System.Text;

namespace OnlineHasher.Tests;

public class PasswordHasherTests
{
    private static string Expected(string password, string salt) =>
        Convert.ToHexString(Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), 1000, HashAlgorithmName.SHA1, 32));

    [Fact]
    public void Hash_WithPasswordAndSalt_ReturnsPbkdf2Hex() =>
        Assert.Equal(Expected("password", "salt"), PasswordHasher.Hash("password", "salt").Hash);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Hash_WithBlankSalt_UsesEmptySalt(string salt) =>
        Assert.Equal(Expected("password", ""), PasswordHasher.Hash("password", salt).Hash);

    [Fact]
    public void Hash_IsDeterministicAndSaltSensitive()
    {
        var a = PasswordHasher.Hash("pw", "s1").Hash;

        Assert.Equal(a, PasswordHasher.Hash("pw", "s1").Hash);
        Assert.NotEqual(a, PasswordHasher.Hash("pw", "s2").Hash);
        Assert.Equal(64, a.Length);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Hash_WithoutPassword_Fails(string password)
    {
        var result = PasswordHasher.Hash(password, "salt");

        Assert.False(result.IsSuccess);
        Assert.Equal("Password is required.", result.Error);
    }

    [Fact]
    public void Hash_WithWhitespacePassword_Succeeds() =>
        Assert.Equal(Expected("   ", "salt"), PasswordHasher.Hash("   ", "salt").Hash);
}
