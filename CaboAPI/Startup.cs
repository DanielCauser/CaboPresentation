using AutoMapper;
using CaboAPI.DTOs;
using CaboAPI.MapperConfig;
using CaboAPI.Options;
using CaboAPI.Services;
using CaboAPI.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;

namespace CaboAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddOptions();

            services.Configure<CaboApiConfiguration>(Configuration);
            services.Configure<ExternalServiceConfiguration>(Configuration.GetSection("ExternalServiceConfiguration"));

            services.AddApplicationInsightsTelemetry();

            services.AddApiVersioning(config =>
            {
                config.ReportApiVersions = true;
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(2, 0);
                // If using in header, we need to remove the version from the routes
//                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson();

            services.AddMvc()
                .AddFluentValidation();

            services.AddSingleton(new MapperConfiguration(mc => { mc.AddProfile(new DtoMappingProfile()); })
                .CreateMapper());

            services.AddTransient<IValidator<TodoCaboCreateDto>, TodoCaboCreateDtoValidator>();

            services.AddSingleton<ITodoCaboService, TodoCaboService>();
            services.AddScoped<ITodoItemService, TodoItemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApiVersioning();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
