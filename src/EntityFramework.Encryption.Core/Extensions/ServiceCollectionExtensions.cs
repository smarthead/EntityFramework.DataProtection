using System;
using System.Collections.Generic;
using EntityFramework.Encryption.Core.Abstractions;
using EntityFramework.Encryption.Core.Customizers;
using EntityFramework.Encryption.Core.Services;
using EntityFramework.Encryption.Core.Services.Aes256;
using EntityFramework.Encryption.Core.Services.Sha512;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.Encryption.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static EncryptionServicesBuilder AddEfEncryption<TContext>(this IServiceCollection services,
            Action<EncryptionServicesOptions> configure = null)
        where TContext : DbContext
        {
            services.AddDbContext<TContext>(
                opt => opt.ReplaceService<IModelCustomizer, EncryptedModelCustomizer>());

            services.AddSingleton<ValueConverterResolver>();
            
            var builder = new EncryptionServicesBuilder(services);

            if(configure != null)
                services.Configure(configure);

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

    public class EncryptionServicesOptions
    {
        public Dictionary<Type, IDataEncryptor> Encryptors { get; set; }
        
        public Dictionary<Type, IDataHasher> Hashers { get; set; }
    }
}