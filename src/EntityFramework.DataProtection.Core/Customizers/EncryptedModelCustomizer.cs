using System.Linq;
using System.Reflection;
using EntityFramework.DataProtection.Core.Abstractions;
using EntityFramework.DataProtection.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFramework.DataProtection.Core.Customizers
{
    public class EncryptedModelCustomizer : ModelCustomizer
    {
        private readonly ValueConverterResolver _valueConverterResolver;

        public EncryptedModelCustomizer(
            ModelCustomizerDependencies dependencies,
            ValueConverterResolver valueConverterResolver) : base(dependencies)
        {
            _valueConverterResolver = valueConverterResolver;
        }
        
        public override void Customize(ModelBuilder modelBuilder, DbContext context)
        {
            base.Customize(modelBuilder, context);
            
            var properties = modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(x => x.GetProperties())
                .Where(x => x.PropertyInfo != null && x.PropertyInfo.IsDefined(typeof(DataProtectionAttribute)))
                .ToArray();
            
            foreach (var property in properties)
            {
                var protectorType = property
                    .PropertyInfo
                    .GetCustomAttribute<DataProtectionAttribute>()
                    .ProtectorType;

                var valueConverter = _valueConverterResolver
                    .Resolve(protectorType);
                
                property.SetValueConverter(valueConverter);
            }

        }
    }
}