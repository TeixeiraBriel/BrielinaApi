using Dominio.Configuration;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SerafinsController : ControllerBase
    {
        #region Variaveis

        public AppSettings _appSettings { get; set; }
        public ITemaRepositorio _temaRepo { get; set; }
        public IComentariosRepositorio _comentarioRepo { get; set; }

        #endregion

        #region Construtor
        public SerafinsController(AppSettings appSettings, ITemaRepositorio temaRepositorio, IComentariosRepositorio comentariosRepositorio)
        {
            _appSettings = appSettings;
            _temaRepo = temaRepositorio;
            _comentarioRepo = comentariosRepositorio;
        }

        #endregion

        #region Requisições

        private int ObterUsuarioId()
        {
            // NameIdentifier = ID numérico do usuário
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(idStr, out int userId))
                return userId;

            throw new UnauthorizedAccessException("ID do usuário não encontrado no token");
        }

        [HttpGet, Route("temas")]
        public async Task<List<TemaDto>> Listar()
        {
            var temasDb = await _temaRepo.ListarAsync();

            return temasDb.Select(t => new TemaDto
            {
                Id = t.Id,
                Livro = t.Livro,
                Responsavel = t.Responsavel?.Nome ?? t.Responsavel?.Usuario ?? null,
                DataApresentacao = t.DataApresentacao
            }).ToList();
        }

        [HttpPost, Route("temas/novo")]
        public async Task<IActionResult> CriarTema([FromBody] CriarTemaDto dto)
        {
            var usuarioId = ObterUsuarioId();

            // exemplo usando Dapper / ADO / Repo
            await _temaRepo.InserirAsync(new TemaModel
            {
                Livro = dto.Livro,
                ResponsavelId = (int?)null,
                DataApresentacao = (DateTime?)null,
                CriadoPorId = usuarioId,
                CriadoEm = DateTime.Now
            });

            return Ok();
        }

        [HttpPost("temas/{id}/assumir-responsavel")]
        public async Task<IActionResult> AssumirResponsavel(int id)
        {
            var usuarioId = ObterUsuarioId();
            await _temaRepo.DefinirResponsavelAsync(id, usuarioId);
            return Ok();
        }

        [HttpPost("temas/{id}/definir-data")]
        public async Task<IActionResult> DefinirData(int id, [FromBody] DateTime data)
        {
            await _temaRepo.DefinirDataApresentacaoAsync(id, data);
            return Ok();
        }

        [HttpGet("temas/{id}/comentarios")]
        public async Task<IEnumerable<ComentarioModel>> ListarComentarios(int id)
        {
            return await _comentarioRepo.ListarPorTemaAsync(id);
        }

        [HttpPost("temas/comentarios")]
        public async Task<IActionResult> CriarComentario([FromBody] CriarComentarioDto dto)
        {
            var usuarioId = ObterUsuarioId();

            await _comentarioRepo.InserirAsync(new ComentarioModel
            {
                TemaId = dto.TemaId,
                UsuarioId = usuarioId,
                Texto = dto.Texto
            });

            return Ok();
        }
        #endregion

    }
}
