using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEChallenge.Application.Interfaces;
using MEChallenge.Application.Services;
using MEChallenge.Application.Validators;
using MEChallenge.Data;
using MEChallenge.Data.Repositories;
using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        }
    }
}
