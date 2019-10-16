using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repositories.Repositories;
using Repositorys.DBContext;
using Services.IServices;
using Services.Services;

namespace S3InnovationAPI
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
            services.AddDbContext<S3InnovationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("S3InnovationDB")));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<ITradeServices, TradeServices>();

            services.AddSingleton<IFileProvider>(
           new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), "Resources/Files")));

            services.AddCors(options =>
            {

                options.AddPolicy("AllowSpecificOrigin",
                    b => b.WithOrigins("http://localhost:4200").WithMethods("POST", "GET","PUT").AllowAnyHeader());

            });
            services.AddMvc()
                .AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Shows UseCors with named policy.
            app.UseCors("AllowSpecificOrigin");
            app.UseCors(options =>
   options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                              async context =>
                              {
                                  context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                  context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                                  var error = context.Features.Get<IExceptionHandlerFeature>();
                                  if (error != null)
                                  {
                                      //context.Response.AddApplicationError(error.Error.Message);
                                      await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                                  }
                              });
                });
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
