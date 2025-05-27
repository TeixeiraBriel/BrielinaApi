using Dominio.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public HomeController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [HttpGet("versao")]
        public string Versao()
        {
            return _appSettings.Version;
        }
    }
}
