using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Repositorio
{
    public class TemaRepositorio : ITemaRepositorio
    {
        private readonly IConfiguration _configuration;

        public TemaRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<TemaModel>> ListarAsync()
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.Temas
                .Select(t => new TemaModel
                {
                    Id = t.Id,
                    Livro = t.Livro,
                    Responsavel = t.Responsavel,
                    ResponsavelId = t.ResponsavelId,
                    DataApresentacao = t.DataApresentacao,
                    CriadoPorId = t.CriadoPorId,
                    CriadoEm = t.CriadoEm,
                    AtualizadoEm = null  // ← força null se coluna não existir
                })
                .ToListAsync();
        }

        public async Task InserirAsync(TemaModel tema)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            _context.Temas.Add(tema);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(TemaModel tema)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            _context.Temas.Update(tema);
            await _context.SaveChangesAsync();
        }

        public async Task DefinirResponsavelAsync(int temaId, int usuarioId)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            var tema = await _context.Temas.FindAsync(temaId);
            if (tema != null)
            {
                tema.ResponsavelId = usuarioId;
                tema.AtualizadoEm = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DefinirDataApresentacaoAsync(int temaId, DateTime data)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            var tema = await _context.Temas.FindAsync(temaId);
            if (tema != null)
            {
                tema.DataApresentacao = data;
                tema.AtualizadoEm = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TemaModel?> ObterPorIdAsync(int id)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            return await _context.Temas
                .Include(t => t.Responsavel)
                .Include(t => t.CriadoPor)
                .Include(t => t.Comentarios)
                    .ThenInclude(c => c.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            using var _context = ContextFactory.OpenContext(_configuration);
            var tema = await _context.Temas.FindAsync(id);
            if (tema != null)
            {
                _context.Temas.Remove(tema);
                await _context.SaveChangesAsync();
            }
        }
    }

}
