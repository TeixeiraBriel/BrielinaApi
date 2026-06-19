using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Infraestrutura.Repositorio
{
    public class MovieReviewRepositorio : IMovieReviewRepositorio
    {
        private readonly IConfiguration _configuration;

        public MovieReviewRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<MovieReviewModel>> ListarPorFilmeAsync(int movieId)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.MovieReviews
                .Include(r => r.Usuario)
                .Where(r => r.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<List<MovieReviewModel>> ListarPorUsuarioAsync(long usuarioId)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.MovieReviews
                .Include(r => r.Usuario)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<MovieReviewModel?> ObterPorIdAsync(long id)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.MovieReviews.FindAsync(id);
        }

        public async Task InserirAsync(MovieReviewModel review)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            _context.MovieReviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(MovieReviewModel review)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            _context.MovieReviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(long id)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            var review = await _context.MovieReviews.FindAsync(id);
            if (review != null)
            {
                _context.MovieReviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<double> ObterMediaRatingAsync(int movieId)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            var medias = await _context.MovieReviews
                .Where(r => r.MovieId == movieId)
                .Select(r => r.Rating)
                .ToListAsync();

            if (!medias.Any()) return 0.0;
            return Math.Round(medias.Average(), 1);
        }

        // Upsert: if Id == 0 -> insert; otherwise update (and ensure the UsuarioId matches when updating).
        public async Task<MovieReviewModel> UpsertAsync(MovieReviewModel review)
        {
            using var _context = ContextFactory.OpenContext(_configuration);

            if (review.Id == 0)
            {
                _context.MovieReviews.Add(review);
                await _context.SaveChangesAsync();
                return review;
            }

            var existing = await _context.MovieReviews.FindAsync(review.Id);
            if (existing == null)
            {
                // Treat as insert if not found
                _context.MovieReviews.Add(review);
                await _context.SaveChangesAsync();
                return review;
            }

            // Ensure the same user can only update their own review (optional enforcement)
            if (existing.UsuarioId != review.UsuarioId)
            {
                throw new UnauthorizedAccessException("Usuário não autorizado a atualizar este review");
            }

            existing.Rating = review.Rating;
            existing.Review = review.Review;
            existing.Recommended = review.Recommended;
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
