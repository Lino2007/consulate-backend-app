using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using DinkToPdf;
using DinkToPdf.Contracts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSI.BusinessLogic.Implementations;
using NSI.BusinessLogic.Interfaces;
using NSI.BusinessLogic.Validators;
using NSI.Cache.Implementations;
using NSI.Cache.Interfaces;
using NSI.DataContracts.Models;
using NSI.Logger.Implementations;
using NSI.Logger.Interfaces;
using NSI.Proxy.Azure;
using NSI.Repository;
using NSI.Repository.Implementations;
using NSI.Repository.Interfaces;
using NSI.REST.Middlewares;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using NSI.REST.Filters;

namespace NSI.REST
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
            services.AddDbContext<DataContext>(p => p.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration);
            services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "NSI Echo API",
                    Description = "Consulate ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Faculty of Electrical Engineering, University of Sarajevo",
                        Url = new Uri("https://etf.unsa.ba/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT license",
                        Url = new Uri("https://choosealicense.com/licenses/mit/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <br/>
                      Enter 'Bearer' [space] and then your token in the text input below. <br />
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddApiExplorer() // Required to redirect base URL to swagger site
                .AddRazorViewEngine() // Required to manipulate swagger view
                .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; });
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            // Logger
            services.AddSingleton<ILoggerAdapter, NLogAdapter>();

            // Cache
            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();
            
            // PDF generator
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // DB and Repositories
            RegisterRepositories(services);

            // Business Layer
            RegisterBusinessLayer(services);

            // Validators
            RegisterValidators(services);

            // Proxy 
            RegisterProxies(services);

            // Filters
            RegisterFilters(services);

            services.AddCors();
        }

        private void RegisterFilters(IServiceCollection services)
        {
            services.AddScoped<CacheCheck>();
        }

        private void RegisterValidators(IServiceCollection services)
        {
            services.AddSingleton(typeof(IValidator<WorkItemDto>), typeof(WorkItemDtoValidator));
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IPermissionsManipulation, PermissionsManipulation>();
            services.AddTransient<IRolesManipulation, RolesManipulation>();
            services.AddTransient<IWorkItemsManipulation, WorkItemsManipulation>();
            services.AddTransient<IRequestsManipulation, RequestsManipulation>();
            services.AddTransient<IUsersManipulation, UsersManipulation>();
            services.AddTransient<IAuthManipulation, AuthManipulation>();
            services.AddTransient<IEmployeeManipulation, EmployeeManipulation>();
            services.AddTransient<IUserPermissionManipulation, UserPermissionManipulation>();
            services.AddTransient<IDocumentsManipulation, DocumentsManipulation>();
            services.AddTransient<IDocumentTypesManipulation, DocumentTypesManipulation>();
            services.AddTransient<IPdfManipulation, PdfManipulation>();
            services.AddTransient<IFilesManipulation, FilesManipulation>();
            services.AddTransient<IBlockchainManipulation, BlockchainManipulation>();
        }

        private void RegisterBusinessLayer(IServiceCollection services)
        {
            services.AddTransient<IPermissionsRepository, PermissionsRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IWorkItemsRepository, WorkItemsRepository>();
            services.AddTransient<IRequestsRepository, RequestsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IUserPermissionRepository, UserPermissionRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IDocumentsRepository, DocumentsRepository>();
            services.AddTransient<IDocumentTypesRepository, DocumentTypesRepository>();
        }

        private void RegisterProxies(IServiceCollection services)
        {
            services.AddSingleton<IAzureProxy, AzureProxy>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSI API"); });

            app.UseCors(options => options
                .WithOrigins((Configuration.GetValue<string>("AllowedOrigins") ?? "").Split(","))
                .AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            // Mvc
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
