using System;
using EF.DataProtection.Services.Aes256;
using EF.DataProtection.Services.Sha512;

namespace EF.DataProtection.Sample.Entities
{
    public class User
    {
        public long Id { get; set; }

        protected User() { }
        
        public User(string phoneNumber, string email, string sensitiveData)
        {
            PhoneNumber = phoneNumber;
            PhoneNumberHash = phoneNumber;
            Email = email;
            SensitiveData = sensitiveData;
        }
        
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