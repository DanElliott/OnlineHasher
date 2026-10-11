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
        Assert.Equal(Expected("password", "salt"), PasswordHasher.Hash("password", "salt"));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Hash_WithBlankSalt_UsesEmptySalt(string salt) =>
        Assert.Equal(Expected("password", ""), PasswordHasher.Hash("password", salt));

    [Fact]
    public void Hash_IsDeterministicAndSaltSensitive()
    {
        var a = PasswordHasher.Hash("pw", "s1");

        Assert.Equal(a, PasswordHasher.Hash("pw", "s1"));
        Assert.NotEqual(a, PasswordHasher.Hash("pw", "s2"));
        Assert.Equal(64, a.Length);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Hash_WithoutPassword_Throws(string password) =>
        Assert.Throws<ArgumentException>(() => PasswordHasher.Hash(password, "salt"));
}
