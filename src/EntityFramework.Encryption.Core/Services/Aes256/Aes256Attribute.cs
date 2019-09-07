using System;
using EntityFramework.Encryption.Core.Abstractions;

namespace EntityFramework.Encryption.Core.Services.Aes256
{
    public class Aes256Attribute : DataProtectionAttribute
    {
        public Aes256Attribute() 
            : base(typeof(Aes256DataEncryptor))
        {
        }
    }
}