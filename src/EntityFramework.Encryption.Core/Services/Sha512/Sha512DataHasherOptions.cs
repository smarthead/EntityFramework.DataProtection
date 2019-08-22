using System.Security.Cryptography;
using System.Text;

namespace EntityFramework.Encryption.Core.Services.Sha512
{
    public class Sha512DataHasherOptions
    {
        public string Password { get; set; }
        
        public byte[] GetBytes() 
            => Encoding
                .Unicode
                .GetBytes(Password);
    }
}