using BrielinaApi.Controladores;
using Dominio;
using Infraestrutura.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BrielinaApi.Controllers
{
    public class RpgCSharpController : ApiController
    {
        public FragmentoRpgComandos FragmentoRpg = new FragmentoRpgComandos();

        [HttpGet]
        [Route("api/RpgCSharp")]
        public List<FragmentoRpg> Get()
        {
            return FragmentoRpg.carregarRegistros();
        }

        [HttpGet]
        [Route("api/RpgCSharp/{id}")]
        public FragmentoRpg Get(string id)
        {
            return FragmentoRpg.carregarRegistroEspecifico(id);
        }

        [HttpPost]
        [Route("api/RpgCSharp")]
        public string Post([FromBody] FragmentoRpg value)
        {
            return FragmentoRpg.cadastrarRegistro(value);
        }

        [HttpPut]
        [Route("api/RpgCSharp/{id}")]
        public Registro Put(string id, [FromBody] FragmentoRpg value)
        {
            return FragmentoRpg.modificarRegistro(value, id);

        }

        [HttpDelete]
        [Route("api/RpgCSharp/{id}")]
        public string Delete(string id)
        {
            return FragmentoRpg.apagarRegistroEspecifico(id);
        }
    }
}