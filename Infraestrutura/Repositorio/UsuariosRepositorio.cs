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
                var result =  await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == user);
                return result;
            }
        }

        public async Task<UsuarioModel> ObterPorEmail(string email)
        {
            using (Context _context = ContextFactory.OpenContext(_configuration))
            {
                return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
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
