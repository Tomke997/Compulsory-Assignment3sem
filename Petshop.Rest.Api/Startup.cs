using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Petshop.Core.ApplicationService;
using Petshop.Core.ApplicationService.Impl;
using Petshop.Core.DomainService;
using Petshop.Core.Entity;
using Petshop.Infrastructure.Data;
using Petshop.Infrastructure.Data.Repositories;
using TodoApi.Helpers;
using TodoApi.Models;

namespace Petshop.Rest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
            JwtSecurityKey.SetSecret("a secret that needs to be at least 16 characters long");       
        }
        
        public IConfiguration Configuration { get; }        
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtSecurityKey.Key,
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.FromMinutes(5) 
                };
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader()
                        .AllowAnyMethod());
            });
       
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<PetshopContex>(
                    opt => opt.UseSqlite("Data Source=petShopDB.db"));
            }
            else
            {
                // Azure SQL database:
                services.AddDbContext<PetshopContex>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }
                    
            services.AddScoped<IRepository<Pet>, PetRepository>();
            services.AddScoped<IRepository<Owner>, OwnerRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();           
            
            services.AddScoped<IService<Pet>, PetService>();
            services.AddScoped<IService<Owner>, OwnerService>();
            services.AddScoped<IService<User>, UserService>();
            
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            
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
                    /*var ctx = scope.ServiceProvider.GetService<PetshopContex>();
                    ctx.Database.EnsureCreated();*/
                }
                app.UseHsts();
            }
          
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}