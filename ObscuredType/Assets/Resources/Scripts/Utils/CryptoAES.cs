using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class CryptoAES
{
    public const string DefaultPassword = "01234567890123456789012345678901"; // length : 32
    public static string IV = "0123456789012345"; // length : 16

    public enum KeySize 
    {
        Byte128 = 128
    }

    private static RijndaelManaged myRijndael = new RijndaelManaged();

    public static string Encrypt(string plainData, string password = DefaultPassword, KeySize keySize = KeySize.Byte128) => Encrypt(Encoding.UTF8.GetBytes(plainData), password, keySize);

    public static string Encrypt(byte[] plainBytes, string password = DefaultPassword, KeySize keySize = KeySize.Byte128)
    {
        myRijndael.Clear();
        myRijndael.Mode = CipherMode.CBC;
        myRijndael.Padding = PaddingMode.PKCS7;
        myRijndael.KeySize = (int)keySize;

        var memoryStream = new MemoryStream();
        var cryptoStream = new CryptoStream(
            memoryStream,
            myRijndael.CreateEncryptor(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(IV)),
            CryptoStreamMode.Write);

        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        cryptoStream.FlushFinalBlock();

        var encryptBytes = memoryStream.ToArray();

        cryptoStream.Close();
        memoryStream.Close();

        return Convert.ToBase64String(encryptBytes);
    }

    public static string Decrypt(string encrypt, string password = DefaultPassword, KeySize keySize = KeySize.Byte128)
    {
        var encryptBytes = Convert.FromBase64String(encrypt);

        myRijndael.Clear();
        myRijndael.Mode = CipherMode.CBC;
        myRijndael.Padding = PaddingMode.PKCS7;
        myRijndael.KeySize = (int)keySize;

        var memoryStream = new MemoryStream(encryptBytes);
        var cryptoStream = new CryptoStream(
            memoryStream,
            myRijndael.CreateDecryptor(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(IV)), 
            CryptoStreamMode.Read);

        var plainBytes = new byte[encryptBytes.Length];
        int plainCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

        cryptoStream.Close();
        memoryStream.Close();

        return Encoding.UTF8.GetString(plainBytes, 0, plainCount);
    }
}
