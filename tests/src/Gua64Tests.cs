using gua64;
using Xunit;


public class Gua64Tests
{
    [Fact]
    public void EncodeTest()
    {
        // Assert
        Assert.Equal("䷙䷴䷹䷗䷙䷯䷮䷍䷈䷈䷻䷽䷎䷋䷱䷌䷟䷐䷨䷲", Gua64.Encode("你好，world"));
    }
}