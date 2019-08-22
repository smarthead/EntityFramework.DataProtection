namespace EntityFramework.Encryption.Core.Abstractions
{
    public interface IDataEncryptor
    {
        string Encrypt(string plainText);

        string Decrypt(string cipherText);
    }
}