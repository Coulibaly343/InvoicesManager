using InvoicesManager.Api.Filters;
using InvoicesManager.Core.Infrastructure;
using InvoicesManager.Core.Invoices.Commands.CreateInvoice;
using InvoicesManager.Infrastructure.Data;
using InvoicesManager.Infrastructure.Data.Repositories;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.IO;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using InvoicesManager.Core.Extensions.AutoMapper;

namespace InvoicesManager.Api
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
            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute))).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();

            ConfigureDatabase(services);
            AddRepositories(services);
            RegisterMediatR(services);
            AddSettings(services);
            AddSwagger(services);
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<InvoicesManagerContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("MsSqlDatabase"),
                        b => b.MigrationsAssembly("InvoicesManager.Api")));
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(
                   typeof(InvoiceRepository).GetTypeInfo().Assembly)
               .Where(c => c.Name.EndsWith("Repository"))
               .AsPublicImplementedInterfaces();
        }

        private void RegisterMediatR(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(CreateInvoiceCommandHandler).GetTypeInfo().Assembly);
        }

        private void AddSettings(IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfig.Initialize());
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Invoices API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Migrate(serviceScope);
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Invoices API V1");
            });
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void Migrate(IServiceScope serviceScope)
        {
            serviceScope.ServiceProvider.GetService<InvoicesManagerContext>().Database.Migrate();
        }
    }
}
