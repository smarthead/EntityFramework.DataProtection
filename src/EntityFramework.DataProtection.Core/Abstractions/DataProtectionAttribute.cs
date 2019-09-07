using System;

namespace EntityFramework.DataProtection.Core.Abstractions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataProtectionAttribute: Attribute
    {
        public Type ProtectorType { get; }

        public DataProtectionAttribute(Type protectorType)
        {
            ProtectorType = typeof(IDataProtector)
                    .IsAssignableFrom(protectorType)
                ? protectorType
                : throw new InvalidOperationException();
        }
    }
}