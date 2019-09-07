using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Encryption.Core.Abstractions;
using Microsoft.Extensions.Options;

namespace EntityFramework.Encryption.Core.Extensions
{
    public class EncryptionsServicesOptionsPostConfigure : IPostConfigureOptions<EncryptionServicesOptions>
    {
        private readonly IEnumerable<IDataEncryptor> _encryptors;
        private readonly IEnumerable<IDataHasher> _hashers;

        public EncryptionsServicesOptionsPostConfigure(IEnumerable<IDataEncryptor> encryptors, IEnumerable<IDataHasher> hashers)
        {
            _encryptors = encryptors;
            _hashers = hashers;
        }
        
        public void PostConfigure(string name, EncryptionServicesOptions options)
        {
            options.Encryptors = _encryptors
                .ToDictionary(x => x.GetType(), x => x);
            
            options.Hashers = _hashers
                .ToDictionary(x => x.GetType(), x => x);
        }
    }
}