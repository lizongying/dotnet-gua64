using System;
using gua64;

namespace sampleApp;

class Program
{
    static void Main(string[] args)
    {
        var result = Gua64.Encode("你好， world");
        Console.WriteLine($"result {result}");
        result = Gua64.Decode("䷙䷴䷹䷗䷙䷯䷮䷍䷈䷈䷻䷽䷎䷋䷱䷌䷟䷐䷨䷲");
        Console.WriteLine($"result {result}");

        var resultVerify = Gua64.Verify("䷙䷴䷹䷗䷙䷯䷮䷍䷈䷈䷻䷽䷎䷋䷱䷌䷟䷐䷨䷲");
        Console.WriteLine($"result {resultVerify}");
        resultVerify = Gua64.Verify("你好， world");
        Console.WriteLine($"result {resultVerify}");
        resultVerify = Gua64.Verify("䷙䷴你好， world");
        Console.WriteLine($"result {resultVerify}");
    }
}
