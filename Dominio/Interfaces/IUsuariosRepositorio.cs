using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<UsuarioModel> ObterUsuario(string user);
        Task RegistrarUsuario(UsuarioModel user);
    }
}
