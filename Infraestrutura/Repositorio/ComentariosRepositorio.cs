using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Repositorio
{
    public class ComentariosRepositorio : IComentariosRepositorio
    {
        private readonly IConfiguration _configuration;

        public ComentariosRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<ComentarioModel>> ListarPorTemaAsync(int temaId)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.Comentarios
                .Include(c => c.Usuario)
                .Where(c => c.TemaId == temaId)
                .OrderBy(c => c.CriadoEm)
                .ToListAsync();
        }

        public async Task InserirAsync(ComentarioModel comentario)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
        }
    }
}
