using MEChallenge.Application.Interfaces;
using MEChallenge.Application.Services;
using MEChallenge.Application.Validators;
using MEChallenge.Configurations;
using MEChallenge.Data;
using MEChallenge.Data.Repositories;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MEChallenge
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
            services.AddControllers();

            services.AddDbContext<ChallengeContext>(opt => opt.UseInMemoryDatabase("MEChallenge"));

            services.AddScoped<IRepository<Order, string>, OrderRepository>();
            services.AddScoped<IRepository<Item, string>, ItemRepository>();
            services.AddScoped<IValidator<Order>, OrderValidator>();
            services.AddScoped<IValidator<Item>, ItemValidator>();

            services.AddTransient<OrderService>();

            services.AddSwaggerConfig();
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options => {
                options.IncludeXmlComments(System.String.Format(@"{0}MEChallenge.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "MEChallenge API v1.0");
                s.RoutePrefix = string.Empty;
            });

            #endregion
        }
    }
}
