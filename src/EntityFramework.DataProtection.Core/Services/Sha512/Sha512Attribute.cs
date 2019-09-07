using EntityFramework.DataProtection.Core.Abstractions;

namespace EntityFramework.DataProtection.Core.Services.Sha512
{
    public class Sha512Attribute : DataProtectionAttribute
    {
        public Sha512Attribute() : base(typeof(Sha512DataHasher))
        {
        }
    }
}