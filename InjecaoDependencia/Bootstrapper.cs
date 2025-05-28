using Dominio.Configuration;
using Dominio.Interfaces;
using Infraestrutura.Repositorio;
using Infraestrutura.Servico;
using InjecaoDependencia.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InjecaoDependencia
{
    public class Bootstrapper
    {
        public Bootstrapper(IServiceCollection services, IConfiguration configuration)
        {
            //Entity
            services.AddConfiguration<AppSettings>(configuration);

            //Services
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<ILogger<string>, Logger<string>>();

            ////Repositories
            services.AddScoped<INarrativaRepositorio, NarrativaRepositorio>();
            services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
        }
    }
}
