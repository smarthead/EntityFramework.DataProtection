using System.Security.Cryptography;
using System.Text;

namespace EF.DataProtection.Services.Aes256
{
    public class Aes256DataEncryptorOptions
    {
        public string Password { get; set; }

        public string Salt { get; set; }

        public int Iterations { get; set; } = 10000;
        
        public int KeyLength { get; set; } = 32;
        
        public byte[] GetBytes()
        {
            var passwordBytes = Encoding.Unicode.GetBytes(Password);
            var saltBytes = Encoding.Unicode.GetBytes(Salt);

            using (var rgb = new Rfc2898DeriveBytes(passwordBytes, saltBytes, Iterations))
                return rgb.GetBytes(KeyLength);
        }
    }
}