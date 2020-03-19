using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;

namespace EF.DataProtection.Core.Services
{
    public class ValueConverterResolver
    {
        private readonly Dictionary<Type, ValueConverter<string,string>> _services;
        
        public ValueConverterResolver(IOptions<EncryptionServicesOptions> options)
        {
            var encryptionValueConverters = options
                .Value
                .Encryptors
                .ToDictionary(
                    x => x.Key,
                    x => new ValueConverter<string, string>(
                        s => x.Value.Encrypt(s),
                        s => x.Value.Decrypt(s))
                );
            
            var hashValueConverter = options
                .Value
                .Hashers
                .ToDictionary(
                    x => x.Key,
                    x => new ValueConverter<string, string>(
                        s => x.Value.Hash(s),
                        s => null)
                );

            _services = encryptionValueConverters
                .Concat(hashValueConverter)
                .ToDictionary(x => x.Key, x => x.Value);
        }
        
        public ValueConverter<string, string> Resolve(Type type)
        {
            var result = _services.TryGetValue(type, out var service);
            if (!result)
                throw new InvalidOperationException("Service not found");
            return service;
        }
    }
}