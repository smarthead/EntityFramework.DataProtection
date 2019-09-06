using EntityFramework.Encryption.Core.Services.Aes256;
using EntityFramework.Encryption.Core.Services.Sha512;

namespace EntityFramework.Encryption.Sample.Entities
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