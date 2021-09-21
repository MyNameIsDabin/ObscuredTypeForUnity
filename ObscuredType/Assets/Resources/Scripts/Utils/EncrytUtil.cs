using System.Text;
using System.Security.Cryptography;

public class EncrytUtil
{
    public static string GetSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                stringBuilder.Append(bytes[i].ToString("x2"));

            return stringBuilder.ToString();
        }
    }

    public static string GetMD5Hash(string rawData)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                stringBuilder.Append(bytes[i].ToString("x2"));

            return stringBuilder.ToString();
        }
    }
}
