namespace EntityFramework.Encryption.Core.Abstractions
{
    public interface IDataEncryptor : IDataProtector
    {
        string Encrypt(string plainText);

        string Decrypt(string cipherText);
    }
}