using EF.DataProtection.Core.Abstractions;

namespace EF.DataProtection.Core.Services.Aes256
{
    public class Aes256Attribute : DataProtectionAttribute
    {
        public Aes256Attribute() 
            : base(typeof(Aes256DataEncryptor))
        {
        }
    }
}