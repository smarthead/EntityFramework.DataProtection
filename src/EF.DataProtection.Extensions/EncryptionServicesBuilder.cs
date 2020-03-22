using Microsoft.Extensions.DependencyInjection;

namespace EF.DataProtection.Extensions
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