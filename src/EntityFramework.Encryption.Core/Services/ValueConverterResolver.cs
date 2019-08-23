using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Encryption.Core.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;

namespace EntityFramework.Encryption.Core.Services
{
    public class ValueConverterResolver
    {
        private readonly Dictionary<Type, ValueConverter<string,string>> _services;
        
        public ValueConverterResolver(IOptions<EncryptionServicesOptions> options)
        {
            _services = options
                .Value
                .Encryptors
                .ToDictionary(
                    x => x.Key,
                    x => new ValueConverter<string, string>(
                        s => x.Value.Encrypt(s),
                        s => x.Value.Decrypt(s))
                );
        }
        
        public ValueConverter<string, string> Resolve(Type type)
        {
            return null;
        }
    }
}