using EF.DataProtection.Abstractions.Attributes;

namespace EF.DataProtection.Services.Aes256
{
    public class Aes256Attribute : DataProtectionAttribute
    {
        public Aes256Attribute() 
            : base(typeof(Aes256DataEncryptor))
        {
        }
    }
}