using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework.DataProtection.Core.Services
{
    public class EncryptionServicesBuilder
    {
        public EncryptionServicesBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }
        
        public IServiceCollection ServiceCollection { get; set; }
    }
}