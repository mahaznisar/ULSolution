namespace ULSolution.WebApi
{
    using ULSolution;
    using ULSolution.WebApi.Extensions;
    using ULSolution.WebApi.Middlewares;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using ULSolution.Core.Contracts.Services;
    using ULSolution.Core.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddSwaggerConfiguration();
            services.AddHealthChecks();
            services.AddCors();
            services.AddTransient<ICalculatorService, CalculatorService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin()
                                       .AllowAnyHeader()
                                       .AllowAnyMethod());

            app.UseSwaggerSetup();
            app.UseRouting();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}