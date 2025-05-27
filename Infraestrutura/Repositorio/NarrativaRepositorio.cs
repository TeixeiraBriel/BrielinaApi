using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Repositorio
{
    public class NarrativaRepositorio : INarrativaRepositorio
    {
        private IConfiguration _configuration;
        public NarrativaRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Narrativa>> ObterTodasNarrativas()
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                Task<List<Narrativa>> list = _context.Narrativas.ToListAsync();
                return await list;
            }
        }
        public async Task<List<Narrativa>> ObterTodasNarrativasFilhas(string idPai)
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                List<Narrativa> list = await _context.Narrativas.Where(x => x.Ramificacoes == idPai).ToListAsync();
                return list;
            }
        }
        public async Task<Narrativa> ObterNarrativaPorId(int Id)
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                return await _context.Narrativas.FirstOrDefaultAsync(x => x.IdNarrativas == Id);
            }
        }
        public async Task CriarNarrativa(Narrativa newNarrativa)
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                _context.Narrativas.Add(newNarrativa);
                var teste = await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteNarrativa(int Id)
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                Narrativa remove = await _context.Narrativas.FirstOrDefaultAsync(x => x.IdNarrativas == Id);;
                _context.Narrativas.Remove(remove);
                var teste = await _context.SaveChangesAsync();
            }
        }
    }
}
