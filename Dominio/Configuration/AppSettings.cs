using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Configuration
{
    public class AppSettings
    {
        public string Version { get; set; }
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
    }
}
