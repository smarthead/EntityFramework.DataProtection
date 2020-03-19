using System;
using EF.DataProtection.Abstractions.Abstractions;

namespace EF.DataProtection.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataProtectionAttribute: Attribute
    {
        public Type ProtectorType { get; }

        protected DataProtectionAttribute(Type protectorType)
        {
            ProtectorType = typeof(IDataProtector)
                    .IsAssignableFrom(protectorType)
                ? protectorType
                : throw new InvalidOperationException();
        }
    }
}