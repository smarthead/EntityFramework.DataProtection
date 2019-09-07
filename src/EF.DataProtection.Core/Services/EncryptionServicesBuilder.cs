using Microsoft.Extensions.DependencyInjection;

namespace EF.DataProtection.Core.Services
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