using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

            // register dbcontext as singleton
            services.AddSingleton<DatabaseContext>();
            // register dependency interfaces with their implementation as singletons
            services.AddSingleton<IBurgersRepo, BurgersRepo>();
            services.AddSingleton<IBurgersService, BurgersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
