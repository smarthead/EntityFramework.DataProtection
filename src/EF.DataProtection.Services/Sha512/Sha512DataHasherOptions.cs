using System.Text;

namespace EF.DataProtection.Services.Sha512
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