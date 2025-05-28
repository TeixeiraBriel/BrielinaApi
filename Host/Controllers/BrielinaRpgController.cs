using Dominio.Configuration;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrielinaRpgController : ControllerBase
    {
        #region Variaveis

        public AppSettings _appSettings { get; set; }
        public INarrativaRepositorio _narrativaRepositorio { get; set; }

        #endregion

        #region Construtor
        public BrielinaRpgController(AppSettings appSettings, INarrativaRepositorio narrativaRepositorio)
        {
            _appSettings = appSettings;
            _narrativaRepositorio = narrativaRepositorio;
        }

        #endregion

        #region Requisições

        [HttpGet, Route("Todos")]
        public async Task<IActionResult> BuscaTodasAulas()
        {
            try
            {
                var retorne = await _narrativaRepositorio.ObterTodasNarrativas();
                return StatusCode(200, retorne);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("TodosFilhos")]
        public async Task<IActionResult> ObterTodasNarrativasFilhas([FromBody] Narrativa NarrativaPai)
        {
            try
            {
                var retorne = await _narrativaRepositorio.ObterTodasNarrativasFilhas(NarrativaPai.IdNarrativas.ToString());
                return StatusCode(200, retorne);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("{id}")]
        public async Task<IActionResult> BuscaAulaPorId([FromBody] int Id)
        {
            try
            {
                var retorne = await _narrativaRepositorio.ObterNarrativaPorId(Id);
                return StatusCode(200, retorne);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("Nova")]
        public async Task<IActionResult> CriarAula([FromBody] Narrativa newNarrativa)
        {
            try
            {
                Narrativa _narrativa = new Narrativa()
                {
                    Titulo = newNarrativa.Titulo != null ? newNarrativa.Titulo : "",
                    Descricao = newNarrativa.Descricao != null ? newNarrativa.Descricao : "",
                    Texto = newNarrativa.Texto != null ? newNarrativa.Texto : "",
                    Ramificacoes = newNarrativa.Ramificacoes != null ? newNarrativa.Ramificacoes : "",
                    Tipo = newNarrativa.Tipo != 0 ? newNarrativa.Tipo : 1,
                    Autor = newNarrativa.Autor != null ? newNarrativa.Autor : "",
                };

                await _narrativaRepositorio.CriarNarrativa(_narrativa);
                return StatusCode(200, _narrativa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> DeleteAula([FromRoute] int Id)
        {
            try
            {
                await _narrativaRepositorio.DeleteNarrativa(Id);
                return StatusCode(200, "Sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

    }
}
