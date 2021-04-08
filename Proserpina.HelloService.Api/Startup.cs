using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Proserpina.HelloService.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/restricted/hello", async context =>
                {
                    if (!context.Request.Headers.ContainsKey("fullname") || !context.Request.Headers.ContainsKey("user-id"))
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }


                    string fullName = context.Request.Headers.FirstOrDefault(x => x.Key == "fullname").Value;

                    await context.Response.WriteAsync($"Hello {fullName}");
                });

                endpoints.MapGet("/public/health", async context =>
                {
                    await context.Response.WriteAsync("I'm Healthy!");
                });
            });
        }
    }
}
