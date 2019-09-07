using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Encryption.Core.Customizers;
using EntityFramework.Encryption.Core.Extensions;
using EntityFramework.Encryption.Sample.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EntityFramework.Encryption.Sample
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
                .UseAes256(opt =>
                {
                    opt.Password = "Really_Strong_Password_For_Data";
                    opt.Salt = "Salt";
                })
                .UseSha512(opt => { opt.Password = "Really_Strong_Password_For_Data"; });
            
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SampleDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}