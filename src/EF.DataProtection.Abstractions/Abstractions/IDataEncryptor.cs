namespace EF.DataProtection.Abstractions.Abstractions
{
    public interface IDataEncryptor : IDataProtector
    {
        string Encrypt(string plainText);

        string Decrypt(string cipherText);
    }
}