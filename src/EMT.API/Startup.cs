using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using EMT.BLL;
using EMT.Common;
using EMT.DAL;
using EMT.DAL.EF;

namespace EMT.API
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
            // ------------------------------------------------------------------------------------
            // Adding: Remove camelCasing on serializing
            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);

            // ------------------------------------------------------------------------------------
            // Adding: Required for add ClientInfo in Serilog
            services.AddHttpContextAccessor();

            // ------------------------------------------------------------------------------------
            // Added support for HttpClient (for api invocations)
            services.AddHttpClient();

            // ------------------------------------------------------------------------------------
            // Add CORS (for invocations from Angular/React/SPAs)
            services.AddCors();

            // ------------------------------------------------------------------------------------
            // Adding Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMT.API", Version = "v1" });
            });

            // ------------------------------------------------------------------------------------
            // Use a custom global configuration class
            services.Configure<MyAppConfig>(Configuration.GetSection("MyAppConfig"));

            // ------------------------------------------------------------------------------------
            // DbContext (Database for Entity Framework Core context)
            string dbConnectionString = Configuration.GetConnectionString("cs01");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(dbConnectionString), ServiceLifetime.Scoped);

            // ------------------------------------------------------------------------------------
            // Service dependency-injection
            services.RegisterBLLServices();  // Custom registration for BLL Services
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMT.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // ------------------------------------------------------------------------------------
            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            // ------------------------------------------------------------------------------------
            // Global Exception Handler
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;
                await context.Response.WriteAsJsonAsync(new
                {
                    ServerErrorMessage = exception.Message,
                    ExceptionMessage = exception.ToString()
                });
            }));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
