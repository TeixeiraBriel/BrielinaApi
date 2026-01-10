using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IComentariosRepositorio
    {
        Task<List<ComentarioModel>> ListarPorTemaAsync(int temaId);
        Task InserirAsync(ComentarioModel comentario);
    }
}
