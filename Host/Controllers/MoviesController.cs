using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepositorio _movieRepo;

        public MoviesController(IMovieRepositorio movieRepositorio)
        {
            _movieRepo = movieRepositorio;
        }

        [HttpGet, Route("movies")]
        public async Task<List<MovieDto>> Listar()
        {
            var movies = await _movieRepo.ListarAsync();

            return movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                Year = m.Year,
                Rating = m.Rating,
                PosterUrl = m.PosterUrl,
                DirectedBy = m.DirectedBy,
                Sinopse = m.Sinopse
            }).ToList();
        }

        [HttpGet("movies/{id}")]
        public async Task<ActionResult<MovieDto>> Obter(int id)
        {
            var movie = await _movieRepo.ObterPorIdAsync(id);
            if (movie == null) return NotFound();

            var dto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Year = movie.Year,
                Rating = movie.Rating,
                PosterUrl = movie.PosterUrl,
                DirectedBy = movie.DirectedBy,
                Sinopse = movie.Sinopse
            };

            return Ok(dto);
        }

        [HttpPost, Route("movies/novo")]
        public async Task<IActionResult> Criar([FromBody] MovieDto dto)
        {
            var model = new MovieModel
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Year = dto.Year,
                Rating = dto.Rating,
                PosterUrl = dto.PosterUrl,
                DirectedBy = dto.DirectedBy,
                Sinopse = dto.Sinopse
            };

            await _movieRepo.InserirAsync(model);

            return CreatedAtAction(nameof(Obter), new { id = model.Id }, null);
        }

        [HttpPut("movies/{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] MovieDto dto)
        {
            var existing = await _movieRepo.ObterPorIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = dto.Title;
            existing.Genre = dto.Genre;
            existing.Year = dto.Year;
            existing.Rating = dto.Rating;
            existing.PosterUrl = dto.PosterUrl;
            existing.DirectedBy = dto.DirectedBy;
            existing.Sinopse = dto.Sinopse;

            await _movieRepo.AtualizarAsync(existing);
            return Ok();
        }

        [HttpDelete("movies/{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var existing = await _movieRepo.ObterPorIdAsync(id);
            if (existing == null) return NotFound();

            await _movieRepo.RemoverAsync(id);
            return Ok();
        }
    }
}
