using System.Collections.Generic;
using UnityEngine;

public class Obscured<T>
{
    private const int ObserverCount = 3;

    private System.Random random = new System.Random();
    private T value;
    private T rollbackValue;
    private int[] criptoKeys = new int[ObserverCount];
    private object[] cloneValues = new object[ObserverCount];

    public static List<Obscured<T>> ObscuredValues = new List<Obscured<T>>();

    public T Value
    {
        get => GetVerifyValue();
        set => UpdateValue(value);
    }

    public Obscured(T value) 
    {
        UpdateValue(value);
        ObscuredValues.Add(this);
    }

    ~Obscured() => ObscuredValues.Remove(this);

    // TODO : 삭제 예정. 테스트 코드
    public void Hack_SetValue(T value) => this.value = value;

    public T GetVerifyValue()
    {
        for (int i = 0; i < cloneValues.Length; i++)
        {
            var cloneValue = cloneValues[i];
            var criptoKey = criptoKeys[i];

            var otherValue = (T)DecryptValue(cloneValue, criptoKey);

            if (!EqualityComparer<T>.Default.Equals(value, otherValue))
                return rollbackValue;
        }

        UpdateValue(value);
        return value;
    }

    public void UpdateValue(T value)
    {
        this.value = value;
        rollbackValue = value;

        for (int i = 0; i < cloneValues.Length; i++)
        {
            criptoKeys[i] = GetRandomCryptoKey();
            cloneValues[i] = EncryptValue(value, criptoKeys[i]);
        }
    }

    private object EncryptValue(T value, int cryptoKey)
    {
        switch (value)
        {
            case char charValue:
                return charValue ^ cryptoKey;

            case int intValue:
                return intValue ^ cryptoKey;

            default:
                return CryptoAES.Encrypt(value.ToString(), EncrytUtil.GetMD5Hash(cryptoKey.ToString()));
        }
    }

    private object DecryptValue(object value, int cryptoKey)
    {
        switch (value)
        {
            case char charValue:
                return charValue ^ cryptoKey;

            case int intValue:
                return intValue ^ cryptoKey;

            default:
                return CryptoAES.Decrypt(value.ToString(), EncrytUtil.GetMD5Hash(cryptoKey.ToString()));
        }
    }

    public int GetRandomCryptoKey() => random.Next();
}
