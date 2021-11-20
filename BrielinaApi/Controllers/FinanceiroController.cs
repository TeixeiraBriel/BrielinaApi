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
    public class FinanceiroController : ApiController
    {
        public FinanceiroComandos financeiro = new FinanceiroComandos();

        [HttpGet]
        [Route("api/Financeiro")]
        public List<Registro> Get()
        {
            return financeiro.carregarRegistros();
        }

        [HttpGet]
        [Route("api/Financeiro/{id}")]
        public Registro Get(string id)
        {
            return financeiro.carregarRegistroEspecifico(id);
        }

        [HttpPost]
        [Route("api/Financeiro")]
        public string Post([FromBody] Registro value)
        {
            return financeiro.cadastrarRegistro(value);
        }

        [HttpPut]
        [Route("api/Financeiro/{id}")]
        public Registro Put(string id, [FromBody] Registro value)
        {
            return financeiro.modificarRegistro(value, id);

        }

        [HttpDelete]
        [Route("api/Financeiro/{id}")]
        public string Delete(string id)
        {
            return financeiro.apagarRegistroEspecifico(id);
        }
    }
}