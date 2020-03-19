namespace EF.DataProtection.Abstractions.Abstractions
{
    public interface IDataHasher : IDataProtector
    {
        string Hash(string plainText);
    }
}