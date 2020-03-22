using EF.DataProtection.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.DataProtection.Sample.Dal
{
    public class SampleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserQuery> UserQuery { get; set; }
        
        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserQuery>()
                .ToQuery(() => UserQuery.FromSqlRaw(@"
                        select Id, PhoneNumber, Email, SensitiveData, PhoneNumberHash from Users"))
                .HasNoKey();
        }
    }
}