using EntityFramework.Encryption.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Encryption.Sample.Dal
{
    public class SampleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .OwnsOne<PersonalData>(x => x.PersonalData);
        }
    }
}