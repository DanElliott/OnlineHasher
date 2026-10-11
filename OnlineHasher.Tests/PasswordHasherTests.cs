namespace OnlineHasher.Tests;

// Expected hashes are literals generated independently with Python:
// hashlib.pbkdf2_hmac('sha1', password.encode('utf-8'), salt.encode('utf-8'), 1000, 32).hex().upper()
// They pin the output contract: changing any fixed parameter or the encoding must fail these tests.
public class PasswordHasherTests
{
    private static string Hash(string password, string salt) =>
        Assert.IsType<HashResult.Success>(PasswordHasher.Hash(password, salt)).Hash;

    private const string PasswordSalt = "6E88BE8BAD7EAE9D9E10AA061224034FED48D03FCBAD968B56006784539D5214";
    private const string PasswordNoSalt = "9723591CD1CFECB0EFD7B027EA93B100334B29862A5D8A101F1B668E369C4C34";
    private const string WhitespaceSalt = "9494894A00434EFDEEF8358D249F56FF2B3DE666340A232916CC9C97DCF20694";
    private const string Pw_S1 = "C231029DD360ED6BA04882E16A7A0FC9655272BE713F0481A5018B80BA9C3550";
    private const string NonAscii = "481B7F4BB34A368B8B7FB2D83B1B0C80DE2A10AE32BC9D5DB70512EE44CB3C4A";

    [Fact]
    public void Hash_WithPasswordAndSalt_ReturnsPbkdf2Hex() =>
        Assert.Equal(PasswordSalt, Hash("password", "salt"));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Hash_WithBlankSalt_UsesEmptySalt(string salt) =>
        Assert.Equal(PasswordNoSalt, Hash("password", salt));

    [Fact]
    public void Hash_IsDeterministicAndSaltSensitive()
    {
        Assert.Equal(Pw_S1, Hash("pw", "s1"));
        Assert.Equal(Pw_S1, Hash("pw", "s1"));
        Assert.NotEqual(Pw_S1, Hash("pw", "s2"));
    }

    [Fact]
    public void Hash_EncodesPasswordAndSaltAsUtf8() =>
        Assert.Equal(NonAscii, Hash("p\u00e4ssw\u00f6rd", "s\u00e4lt"));

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Hash_WithoutPassword_Fails(string password)
    {
        var result = PasswordHasher.Hash(password, "salt");

        Assert.Equal("Password is required.", Assert.IsType<HashResult.Failure>(result).Error);
    }

    [Fact]
    public void Hash_WithWhitespacePassword_Succeeds() =>
        Assert.Equal(WhitespaceSalt, Hash("   ", "salt"));
}
