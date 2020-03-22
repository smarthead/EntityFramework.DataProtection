using System;
using System.Linq;
using EF.DataProtection.Abstractions.Abstractions;
using EF.DataProtection.Core.Customizers;
using EF.DataProtection.Core.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EF.DataProtection.Extensions
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
        public static EncryptionServicesBuilder AddDataEncryptor<TImplementation, TOptions>(
            this EncryptionServicesBuilder builder, Action<TOptions> configure = null)
            where TImplementation : IDataEncryptor
            where TOptions : class =>
            builder.AddDataProtector(typeof(IDataEncryptor), typeof(TImplementation), configure);

        public static EncryptionServicesBuilder AddDataHasher<TImplementation, TOptions>(
            this EncryptionServicesBuilder builder, Action<TOptions> configure = null)
            where TImplementation : IDataHasher
            where TOptions : class =>
            builder.AddDataProtector(typeof(IDataHasher), typeof(TImplementation), configure);

        public static EncryptionServicesBuilder AddDataProtector<TOptions>(
            this EncryptionServicesBuilder builder, Type dataProtector, 
            Type implementation, Action<TOptions> configure = null)
            where TOptions : class
        {
            
            builder
                    .ServiceCollection
                    .AddSingleton(dataProtector, implementation);

            if (configure != null)
                builder.ServiceCollection.Configure(configure);

            return builder;
        }
    }
}