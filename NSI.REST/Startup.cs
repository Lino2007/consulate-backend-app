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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(Configuration, "AzureAd");
            services.AddControllers();
            services.AddSwaggerGen();

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

            // DB and Repositories
            RegisterRepositories(services);

            // Business Layer
            RegisterBusinessLayer(services);

            // Validators
            RegisterValidators(services);

            // Proxy 
            RegisterProxies(services);
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
        }

        private void RegisterBusinessLayer(IServiceCollection services)
        {
            services.AddTransient<IPermissionsRepository, PermissionsRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IWorkItemsRepository, WorkItemsRepository>();
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSI API");
            });

            app.UseCors(options => options.WithOrigins((Configuration.GetValue<string>("AllowedOrigins") ?? "").Split(","))
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            // Mvc
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}