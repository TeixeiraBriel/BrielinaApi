using Dominio;
using Infraestrutura.Entidades;
using System.Collections.Generic;
using System.Web.Http;

namespace BrielinaApi.Controllers
{
    public class AnimesController : ApiController
    {
        Dependencias _dependencias = new Dependencias();
        
        // GET api/values
        public List<Anime> Get()
        {
            return _dependencias.animes.carregarAlunos();
        }

        // GET api/values/5
        public Anime Get(string id)
        {
            return _dependencias.animes.carregarAlunoEspecifico(id);
        }

        // POST api/values
        public string Post([FromBody]Anime value)
        {
            return _dependencias.animes.cadastrarAluno(value);
        }

        // PUT api/values/5
        public Anime Put(string id, [FromBody]Anime value)
        {
            return _dependencias.animes.modificarAnime(value, id);
            
        }

        // DELETE api/values/5
        public string Delete(string id)
        {
            return _dependencias.animes.apagarAlunoEspecifico(id);
        }
    }
}
