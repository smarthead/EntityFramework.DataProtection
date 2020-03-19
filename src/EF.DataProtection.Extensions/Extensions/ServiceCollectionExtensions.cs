using System;
using EF.DataProtection.Abstractions.Abstractions;
using EF.DataProtection.Core.Customizers;
using EF.DataProtection.Core.Services;
using EF.DataProtection.Services.Aes256;
using EF.DataProtection.Services.Sha512;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EF.DataProtection.Extensions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static EncryptionServicesBuilder AddEfEncryption(this IServiceCollection services,
            Action<EncryptionServicesOptions> configure = null)
        {
            services.AddSingleton<ValueConverterResolver>();
            services.AddSingleton<IModelCustomizer, EncryptedModelCustomizer>();
            
            var builder = new EncryptionServicesBuilder(services);

            if(configure != null)
                services.Configure(configure);
            
            services.ConfigureOptions<EncryptionsServicesOptionsPostConfigure>();
            
            return builder;
        }
    }
    
    public static class EncryptionServicesBuilderExtensions
    {
        public static EncryptionServicesBuilder UseAes256(this EncryptionServicesBuilder builder,
            Action<Aes256DataEncryptorOptions> configure = null)
        {
            builder.ServiceCollection.AddSingleton<IDataEncryptor, Aes256DataEncryptor>();
            
            if (configure != null)
                builder.ServiceCollection.Configure(configure);

            return builder;
        }
        
        public static EncryptionServicesBuilder UseSha512(this EncryptionServicesBuilder builder,
            Action<Sha512DataHasherOptions> configure = null)
        {
            builder.ServiceCollection.AddSingleton<IDataHasher, Sha512DataHasher>();
            
            if (configure != null)
                builder.ServiceCollection.Configure(configure);

            return builder;
        }
    }
}