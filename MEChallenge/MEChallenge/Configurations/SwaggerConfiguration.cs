using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MEChallenge.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s => {
                s.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "MEChallenge API",
                    Description = "API do desafio C# do Mercado Eletrônico",
                });
            });
        }
    }
}
