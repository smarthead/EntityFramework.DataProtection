namespace EntityFramework.Encryption.Core.Abstractions
{
    public interface IDataHasher
    {
        string Hash(string plainText);
    }
}