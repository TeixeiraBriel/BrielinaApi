using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<UsuarioModel> ObterUsuario(string user);
        Task<UsuarioModel> ObterPorEmail(string email);
        Task RegistrarUsuario(UsuarioModel user);
    }
}
