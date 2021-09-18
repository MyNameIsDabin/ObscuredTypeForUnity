using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class CryptoAES
{
    private static readonly string Password = "3ds1s334e4dcc7c4yz4554e732983h";

    public enum KeySize 
    {
        Byte128 = 128
    }

    private static RijndaelManaged myRijndael = new RijndaelManaged();

    public static string GetPasswordBySize(KeySize keySize = KeySize.Byte128) => Password;

    public static string Encrypt(string plainData, KeySize keySize = KeySize.Byte128) => Encrypt(Encoding.UTF8.GetBytes(plainData), keySize);

    public static string Encrypt(byte[] plainBytes, KeySize keySize = KeySize.Byte128)
    {
        var key = GetPasswordBySize(keySize);

        myRijndael.Clear();
        myRijndael.Mode = CipherMode.CBC;
        myRijndael.Padding = PaddingMode.PKCS7;
        myRijndael.KeySize = 128;

        var memoryStream = new MemoryStream();
        var cryptoStream = new CryptoStream(
            memoryStream,
            myRijndael.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(key)),
            CryptoStreamMode.Write);

        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
        cryptoStream.FlushFinalBlock();

        var encryptBytes = memoryStream.ToArray();

        cryptoStream.Close();
        memoryStream.Close();

        return Convert.ToBase64String(encryptBytes);
    }

    public static string Decrypt(string encrypt, KeySize keySize = KeySize.Byte128)
    {
        var key = GetPasswordBySize(keySize);

        var encryptBytes = Convert.FromBase64String(encrypt);

        myRijndael.Clear();
        myRijndael.Mode = CipherMode.CBC;
        myRijndael.Padding = PaddingMode.PKCS7;
        myRijndael.KeySize = 128;

        var memoryStream = new MemoryStream(encryptBytes);
        var cryptoStream = new CryptoStream(
            memoryStream,
            myRijndael.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(key)), 
            CryptoStreamMode.Read);

        var plainBytes = new byte[encryptBytes.Length];
        int plainCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

        cryptoStream.Close();
        memoryStream.Close();

        return Encoding.UTF8.GetString(plainBytes, 0, plainCount);
    }
}
