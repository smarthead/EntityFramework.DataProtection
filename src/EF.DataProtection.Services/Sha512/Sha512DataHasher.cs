using System;
using System.Security.Cryptography;
using System.Text;
using EF.DataProtection.Abstractions.Abstractions;
using Microsoft.Extensions.Options;

namespace EF.DataProtection.Services.Sha512
{
    public class Sha512DataHasher : IDataHasher
    {
        private readonly byte[] _key;
        
        public Sha512DataHasher(byte[] key)
        {
            _key = key;
        }
        
        public Sha512DataHasher(IOptions<Sha512DataHasherOptions> options) 
            : this(options.Value.GetBytes())
        {
        }
        
        public string Hash(string plainText)
        {
            using (var algorithm = new HMACSHA512(_key))
            {
                var bytes = Encoding.UTF8.GetBytes(plainText);
                var hash = algorithm.ComputeHash(bytes);

                return GetHashString(hash);
            }
        }

        private static string GetHashString(byte[] hash)
            => BitConverter.ToString(hash)
                .Replace("-", string.Empty)
                .ToUpperInvariant();
    }
}