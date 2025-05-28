using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infraestrutura.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private IConfiguration _configuration;
        public UsuariosRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UsuarioModel> ObterUsuario(string user)
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                return await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == user);
            }
        }

        public async Task RegistrarUsuario(UsuarioModel user)
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
