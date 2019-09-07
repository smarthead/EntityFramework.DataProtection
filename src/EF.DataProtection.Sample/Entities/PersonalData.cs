using EF.DataProtection.Core.Services.Aes256;
using EF.DataProtection.Core.Services.Sha512;

namespace EF.DataProtection.Sample.Entities
{
    public class PersonalData
    {
        [Aes256]
        public string PhoneNumber { get; set; }

        [Aes256]
        public string Email { get; set; }

        [Aes256]
        public string SensitiveData { get; set; }

        [Sha512]
        public string PhoneNumberHash { get; protected set; }
    }
}