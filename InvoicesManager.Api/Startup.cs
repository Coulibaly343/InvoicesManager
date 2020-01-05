using InvoicesManager.Api.Filters;
using InvoicesManager.Core.Infrastructure;
using InvoicesManager.Infrastructure.Data;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

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
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<InvoicesManagerContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("MsSqlDb"),
                        b => b.MigrationsAssembly("InvoicesManager.Api")));
        }

        private void AddRepositories(IServiceCollection services)
        {
            //services.RegisterAssemblyPublicNonGenericClasses(
            //       typeof(InvoiceRepository).GetTypeInfo().Assembly)
            //   .Where(c => c.Name.EndsWith("Repository"))
            //   .AsPublicImplementedInterfaces();
        }

        private void RegisterMediatR(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            //services.AddMediatR(typeof(GetVisitQueryHandler).GetTypeInfo().Assembly);
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
