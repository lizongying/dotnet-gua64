namespace gua64;

using System.Text;

public class Gua64
{
    private const string Gua = "䷁䷖䷇䷓䷏䷢䷬䷋" +
                               "䷎䷳䷦䷴䷽䷷䷞䷠" +
                               "䷆䷃䷜䷺䷧䷿䷮䷅" +
                               "䷭䷑䷯䷸䷟䷱䷛䷫" +
                               "䷗䷚䷂䷩䷲䷔䷐䷘" +
                               "䷣䷕䷾䷤䷶䷝䷰䷌" +
                               "䷒䷨䷻䷼䷵䷥䷹䷉" +
                               "䷊䷙䷄䷈䷡䷍䷪䷀";

    public static string Encode(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        var encoded = new List<string>();
        var len = bytes.Length;
        for (var i = 0; i < len; i += 3)
        {
            if (i + 3 <= len)
            {
                encoded.Add(Gua[bytes[i] >> 2].ToString());
                encoded.Add(Gua[(bytes[i] & 0x3) << 4 | (bytes[i + 1] >> 4)].ToString());
                encoded.Add(Gua[(bytes[i + 1] & 0xf) << 2 | (bytes[i + 2] >> 6)].ToString());
                encoded.Add(Gua[bytes[i + 2] & 0x3f].ToString());
                continue;
            }

            if (i + 3 == len + 1)
            {
                encoded.Add(Gua[bytes[i] >> 2].ToString());
                encoded.Add(Gua[(bytes[i] & 0x3) << 4 | (bytes[i + 1] >> 4)].ToString());
                encoded.Add(Gua[(bytes[i + 1] & 0xf) << 2].ToString());
                encoded.Add("〇");
                continue;
            }

            if (i + 3 == len + 2)
            {
                encoded.Add(Gua[bytes[i] >> 2].ToString());
                encoded.Add(Gua[(bytes[i] & 0x3) << 4].ToString());
                encoded.Add("〇");
                encoded.Add("〇");
            }
        }

        return string.Join("", encoded);
    }

    public static string Decode(string str)
    {
        var gua64dict = new Dictionary<char, int>();
        for (var i = 0; i < Gua.Length; i++)
        {
            gua64dict[Gua[i]] = i;
        }

        var b = str.Select(c => gua64dict.GetValueOrDefault(c, 255)).ToList();

        var encoded = new List<int>();
        for (var i = 0; i < b.Count; i += 4)
        {
            encoded.Add((b[i] & 0x3f) << 2 | (b[i + 1] >> 4 & 0x3));
            if (b[i + 2] != 255)
            {
                encoded.Add((b[i + 1] & 0xf) << 4 | (b[i + 2] >> 2 & 0xf));
            }

            if (b[i + 3] != 255)
            {
                encoded.Add((b[i + 2] & 0x3) << 6 | (b[i + 3] & 0x3f));
            }
        }

        var bytes = new byte[encoded.Count];
        for (var i = 0; i < encoded.Count; i++)
        {
            bytes[i] = (byte)encoded[i];
        }

        return Encoding.UTF8.GetString(bytes);
    }

    public static bool Verify(string str)
    {
        var array = Gua.ToCharArray();
        Array.Resize(ref array, array.Length + 1);
        array[^1] = '〇';
        return str.All(c => Array.Exists(array, element => element == c));
    }
}