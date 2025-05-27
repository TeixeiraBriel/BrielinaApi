using Dominio.Configuration;
using Dominio.Interfaces;
using Infraestrutura.Servico;
using InjecaoDependencia.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            //services.AddScoped<ILogger<string>, Logger<string>>();
            //services.AddScoped<IExecutor, Executor>();

            ////Repositories
            //services.AddScoped<ISerafinsHubRepositorio, SerafinsHubRepositorio>();
            //services.AddScoped<IAulaRepositorio, AulaRepositorio>();
            //services.AddScoped<INarrativaRepositorio, NarrativaRepositorio>();
            //services.AddScoped<ISessaoRepositorio, SessaoRepositorio>();
        }
    }
}
