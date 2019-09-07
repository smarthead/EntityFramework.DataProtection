namespace EntityFramework.Encryption.Core.Abstractions
{
    public interface IDataHasher : IDataProtector
    {
        string Hash(string plainText);
    }
}