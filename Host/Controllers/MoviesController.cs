using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepositorio _movieRepo;
        private readonly IMovieReviewRepositorio _reviewRepo;

        public MoviesController(IMovieRepositorio movieRepositorio, IMovieReviewRepositorio movieReviewRepositorio)
        {
            _movieRepo = movieRepositorio;
            _reviewRepo = movieReviewRepositorio;
        }

        private int ObterUsuarioId()
        {
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(idStr, out int userId)) return userId;
            throw new UnauthorizedAccessException("ID do usuário não encontrado no token");
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

        // POST/PUT /movies/reviews (front doesn't care if insert or update; send the dto with Id==0 for insert)
        [HttpPost("movies/reviews")]
        public async Task<IActionResult> UpsertReview([FromBody] MovieReviewDto dto)
        {
            var usuarioId = ObterUsuarioId();

            var movie = await _movieRepo.ObterPorIdAsync(dto.MovieId);
            if (movie == null) return NotFound();

            var reviewModel = new MovieReviewModel
            {
                Id = dto.Id,
                MovieId = dto.MovieId,
                UsuarioId = usuarioId,
                Rating = dto.Rating,
                Review = dto.Review,
                Recommended = dto.Recommended,
                CreatedAt = DateTime.Now
            };

            var saved = await _reviewRepo.UpsertAsync(reviewModel);

            // Recalcula média e atualiza rating do filme
            var media = await _reviewRepo.ObterMediaRatingAsync(dto.MovieId);
            movie.Rating = media;
            await _movieRepo.AtualizarAsync(movie);

            return Ok(new { reviewId = saved.Id, movieRating = media });
        }

        // GET /movies/{id}/reviews
        [HttpGet("movies/{id}/reviews")]
        public async Task<List<MovieReviewDto>> ListarReviewsDoFilme(int id)
        {
            var reviews = await _reviewRepo.ListarPorFilmeAsync(id);
            return reviews.Select(r => new MovieReviewDto
            {
                Id = r.Id,
                MovieId = r.MovieId,
                UsuarioId = r.UsuarioId,
                UserName = r.Usuario?.Nome ?? r.Usuario?.Usuario ?? string.Empty,
                Rating = r.Rating,
                Review = r.Review,
                Recommended = r.Recommended
            }).ToList();
        }

        // GET /movies/reviews/usuario/{usuarioId}
        [HttpGet("movies/reviews/usuario/{usuarioId}")]
        public async Task<List<MovieReviewDto>> ListarReviewsPorUsuario(long usuarioId)
        {
            var reviews = await _reviewRepo.ListarPorUsuarioAsync(usuarioId);
            return reviews.Select(r => new MovieReviewDto
            {
                Id = r.Id,
                MovieId = r.MovieId,
                UsuarioId = r.UsuarioId,
                UserName = r.Usuario?.Nome ?? r.Usuario?.Usuario ?? string.Empty,
                Rating = r.Rating,
                Review = r.Review,
                Recommended = r.Recommended
            }).ToList();
        }

        // GET /movies/reviews/me
        [HttpGet("movies/reviews/me")]
        public async Task<List<MovieReviewDto>> ListarMeusReviews()
        {
            var usuarioId = ObterUsuarioId();
            var reviews = await _reviewRepo.ListarPorUsuarioAsync(usuarioId);
            return reviews.Select(r => new MovieReviewDto
            {
                Id = r.Id,
                MovieId = r.MovieId,
                UsuarioId = r.UsuarioId,
                UserName = r.Usuario?.Nome ?? r.Usuario?.Usuario ?? string.Empty,
                Rating = r.Rating,
                Review = r.Review,
                Recommended = r.Recommended
            }).ToList();
        }
    }
}
