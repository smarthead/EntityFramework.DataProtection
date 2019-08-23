using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFramework.Encryption.Core.Customizers
{
    public class EncryptedModelCustomizer : IModelCustomizer
    {
        public void Customize(ModelBuilder modelBuilder, DbContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}