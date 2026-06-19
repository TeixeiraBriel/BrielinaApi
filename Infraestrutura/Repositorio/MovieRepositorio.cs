using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Repositorio
{
    public class MovieRepositorio : IMovieRepositorio
    {
        private readonly IConfiguration _configuration;

        public MovieRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<MovieModel>> ListarAsync()
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.Movies.ToListAsync();
        }

        public async Task<MovieModel?> ObterPorIdAsync(int id)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.Movies.FindAsync(id);
        }

        public async Task InserirAsync(MovieModel movie)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(MovieModel movie)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
