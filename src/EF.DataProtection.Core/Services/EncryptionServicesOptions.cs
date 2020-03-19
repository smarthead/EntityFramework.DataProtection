using System;
using System.Collections.Generic;
using EF.DataProtection.Abstractions.Abstractions;

namespace EF.DataProtection.Core.Services
{
    public class EncryptionServicesOptions
    {
        public Dictionary<Type, IDataEncryptor> Encryptors { get; set; }
        
        public Dictionary<Type, IDataHasher> Hashers { get; set; }
    }
}