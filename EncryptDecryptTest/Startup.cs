using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EncryptDecryptTest.Filters;
using EncryptDecryptTest.Middlewares;
using EncryptDecryptTest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EncryptDecryptTest
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
            services.AddMvc(config =>
            {
                config.Filters.Add(new ResponseFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseMiddleware(typeof(RequestResponseModifierMiddleware));
            //app.UseMiddleware(typeof(RequestResponseLoggingMiddleware));
            app.UseHttpsRedirection();


            //app.Use(async (context, next) =>
            //{
            //    var originalBodyStream = context.Response.Body;

            //    try
            //    {
            //        try
            //        {
            //            using (var memStream = new MemoryStream())
            //            {
            //                context.Response.Body = memStream;

            //                await next();

            //                memStream.Position = 0;
            //                string responseBody = new StreamReader(memStream).ReadToEnd();

            //                memStream.Position = 0;
            //                await memStream.CopyToAsync(originalBodyStream);


            //            }

            //        }
            //        finally
            //        {
            //            context.Response.Body = originalBodyStream;
            //        }

            //    }
            //    finally
            //    {
            //        context.Response.Body = originalBodyStream;
            //    }

            //});

            app.UseMvc();
        }
    }
}
