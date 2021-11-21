using Api.Services;
using Business;
using Business.Interface;
using Business.Interface.Services;
using Business.Services;
using Data.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserIdentity.Data;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            services.AddDbContext<DomainDbContext>(opt =>
                opt
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("sqlConnection2")));

            services.AddRepository();

            services.AddAuthentication();

            services.ConfigureIdentity();

            services.ConfigureAutomapper();

            services.AddControllers();

            services.AddCors();

            services.ConfigureJWT(Configuration);

            services.AddMvc();

            services.AddScoped<IAuthManager, AuthManager>();

            services.AddTransient<IProfileRegistrationService, ProfileRegistrationService>();

            services.AddTransient<IOrderProcessingService, OrderProcessingService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseOptions();

            app.UseCors(b => b.AllowAnyOrigin());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
