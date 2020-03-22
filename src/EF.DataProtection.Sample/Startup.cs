using EF.DataProtection.Extensions;
using EF.DataProtection.Sample.Dal;
using EF.DataProtection.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EF.DataProtection.Sample
{
    public class Startup
    {
        private SqliteConnection _connection;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            services
                .AddEntityFrameworkSqlite()
                .AddDbContext<SampleDbContext>(
                    (p, opt) =>
                    {
                        opt.UseSqlite(_connection)
                            .UseInternalServiceProvider(p);
                    })
                .AddEfEncryption()
                .AddAes256(opt =>
                {
                    opt.Password = "Really_Strong_Password_For_Data";
                    opt.Salt = "Salt";
                })
                .AddSha512(opt => { opt.Password = "Really_Strong_Password_For_Data"; });
            
            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SampleDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}