using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using PhoneBook.Logic;
using PhoneBook.Logic.Command;
using PhoneBook.Logic.Profiles;
using PhoneBook.Logic.Queries;
using PhoneBook.Logic.Validators;
using PhoneBook.Web.Filters;
using Serilog;
using System.Collections.Generic;

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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:44359/";
                    //options.Authority = "https://security.phonebook.btrc.local/";
                    options.RequireHttpsMetadata = true;
                    options.Audience = "Phonebook api";
                });
            services.AddAuthorization();
            services.AddMediatR(typeof(GetEmployeesByName).Assembly);
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.Title = "Phone Book";
                cfg.AddSecurity("oauth", new[] { "phonebook_api" }, new OpenApiSecurityScheme()
                {
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    AuthorizationUrl = "https://localhost:44359/connect/authorize",
                    //AuthorizationUrl = "https://security.phonebook.btrc.local/connect/authorize",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"phonebook_api", "Access to phonebook api" }
                    }
                });
            });
            services.AddPhoneBookServices();
            services.AddMemoryCache();
            services.AddLogging();
            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Filters.Clear();
                opt.Filters.Add(typeof(GlobalExceptionFilter));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<EmployeeValidator>();
                    cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMediator mediator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt =>
                opt.WithOrigins("http://phonebook.btrc.local")
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

            app.UseAuthentication();

            app.UseOpenApi().UseSwaggerUi3(opt => opt.OAuth2Client = new OAuth2ClientSettings()
            {
                AppName = "Phone Book",
                ClientId = "swagger"
            }); ;

            mediator.Send(new CreateDatabaseCommand()).Wait();

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
