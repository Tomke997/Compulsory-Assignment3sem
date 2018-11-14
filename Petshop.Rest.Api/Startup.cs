using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Petshop.Core.ApplicationService;
using Petshop.Core.ApplicationService.Impl;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using Petshop.Infrastructure.Data;
using Petshop.Infrastructure.Data.Repositories;

namespace Petshop.Rest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        private IHostingEnvironment _env { get; set; }

        
        public void ConfigureServices(IServiceCollection services)
        {
           
            if (_env.IsDevelopment())
	        {		        
	            services.AddDbContext<PetshopContex>(
	                opt => opt.UseSqlite("Data Source=petShopDB.db"));
	        }

	        else if (_env.IsProduction())
	        {
		        services.AddDbContext<PetshopContex>(
			        opt => opt
				        .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
	        }

            
            
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerService, OwnerService>();
            
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader()
                        .AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {          
            if (env.IsDevelopment())
            {               
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetshopContex>();
                    DBSeed.SeedDB(ctx);
                }
            }
            else
            {            
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<PetshopContex>();
                    ctx.Database.EnsureCreated();
                }
                app.UseHsts();
            }
          
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}