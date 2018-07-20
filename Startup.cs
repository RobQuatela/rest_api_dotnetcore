using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using rest_api_dotnetcore.Config;
using rest_api_dotnetcore.Repositories;
using rest_api_dotnetcore.Services;

namespace rest_api_dotnetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            // mongodb to service collection
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDB"));
            // configure json jwt information to POCO
            services.Configure<JwtCertificate>(Configuration.GetSection("JwtCertificate"));

            // register dbcontext as singleton
            services.AddScoped<DatabaseContext>();
            // register seeddatabase class with container
            services.AddScoped<SeedData>();
            // register dependency interfaces with their implementation as singletons
            services.AddScoped<IBurgersRepo, BurgersRepo>();
            services.AddScoped<IBurgersService, BurgersService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();

            //add jwt authorization capablity to IServiceProvider
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    //create token validation parameters
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration.GetSection("JwtCertificate:issuer").Value,
                        ValidAudience = Configuration.GetSection("JwtCertificate:audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JwtCertificate:secret").Value))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
