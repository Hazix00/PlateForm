using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlateForm.DataStore.EF;

namespace PlateForm.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<BugsContext>(options =>
                {
                    options.UseInMemoryDatabase("Bugs");
                });
            }
            services.AddControllers();
            services.AddApiVersioning(options =>
           {
               options.ReportApiVersions = true;
               options.AssumeDefaultVersionWhenUnspecified = true;
               options.DefaultApiVersion = new ApiVersion(1, 0);
               //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
           });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddSwaggerGen(options =>
            {
                for (int i = 1; i <= 2; i++)
                {
                    options.SwaggerDoc($"v{i}", new OpenApiInfo { Title = $"My Web API v{i}", Version = $"version {i}" });
                }
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:44395")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BugsContext bugsContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Create in-memory database for dev envirement
                bugsContext.Database.EnsureDeleted();
                bugsContext.Database.EnsureCreated();
                //Configure OpenApi
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        options.SwaggerEndpoint($"/swagger/v{i}/swagger.json", $"WebApi v{i}");
                    }
                });
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
