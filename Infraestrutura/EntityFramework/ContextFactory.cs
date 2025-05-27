using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.EntityFramework
{
    public class ContextFactory
    {
        public static Context OpenContext(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableServiceProviderCaching();

            return new Context(optionsBuilder.Options, configuration);
        }
    }
}
