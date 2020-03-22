using System;
using EF.DataProtection.Abstractions.Abstractions;
using EF.DataProtection.Extensions;
using EF.DataProtection.Services.Aes256;
using EF.DataProtection.Services.Sha512;
using Microsoft.Extensions.DependencyInjection;

namespace EF.DataProtection.Services.Extensions
{
    public static class EncryptionServicesBuilderExtensions
    {
        public static EncryptionServicesBuilder AddAes256(this EncryptionServicesBuilder builder,
            Action<Aes256DataEncryptorOptions> configure = null)
        {
            return builder
                .AddDataEncryptor<Aes256DataEncryptor, Aes256DataEncryptorOptions>(configure);
        }
        
        public static EncryptionServicesBuilder AddSha512(this EncryptionServicesBuilder builder,
            Action<Sha512DataHasherOptions> configure = null)
        {
            return builder
                .AddDataHasher<Sha512DataHasher, Sha512DataHasherOptions>(configure);
        }
    }
}