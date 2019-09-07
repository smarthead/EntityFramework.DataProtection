using System;
using EntityFramework.Encryption.Core.Abstractions;

namespace EntityFramework.Encryption.Core.Services.Sha512
{
    public class Sha512Attribute : DataProtectionAttribute
    {
        public Sha512Attribute() : base(typeof(Sha512DataHasher))
        {
        }
    }
}