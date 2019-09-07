namespace EF.DataProtection.Core.Abstractions
{
    public interface IDataHasher : IDataProtector
    {
        string Hash(string plainText);
    }
}