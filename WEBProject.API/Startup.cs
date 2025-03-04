﻿using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using WEBProject.API.Data;
using WEBProject.API.Helpers;

namespace WEBProject.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => { 
                    opt.SerializerSettings.ReferenceLoopHandling =
                     Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddAutoMapper();
            services.AddScoped<IApartmentBookRepository, ApartmentBookRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
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
            else
            {
                 app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null) 
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
             }                   
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources/Images")),

            });

            app.UseAuthentication();
            app.UseMvc();

            InitialData.Initialize(app);
        }
    }
}
