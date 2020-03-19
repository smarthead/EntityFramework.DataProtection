using EF.DataProtection.Abstractions.Attributes;

namespace EF.DataProtection.Services.Sha512
{
    public class Sha512Attribute : DataProtectionAttribute
    {
        public Sha512Attribute() : base(typeof(Sha512DataHasher))
        {
        }
    }
}