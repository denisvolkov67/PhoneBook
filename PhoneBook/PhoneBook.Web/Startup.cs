using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NJsonSchema;
using PhoneBook.Logic.Profiles;
using PhoneBook.Logic.Queries;
using PhoneBook.Web.Filters;
using Serilog;

namespace PhoneBook.Web
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
            services.AddMediatR(typeof(GetUserByLogin).Assembly);
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.Title = "Phone Book";
            });
            services.AddLogging();
            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Filters.Clear();
                opt.Filters.Add(typeof(GlobalExceptionFilter));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt =>
                opt.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://phonebook.btrc.local")
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials());

            app.UseOpenApi().UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Debug()
                //.WriteTo.SQLite(Path.Combine(Environment.CurrentDirectory, @"\phonebook.db"))
                .CreateLogger();
        }
    }
}
