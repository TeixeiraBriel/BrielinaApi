using Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IMovieRepositorio
    {
        Task<List<MovieModel>> ListarAsync();
        Task<MovieModel?> ObterPorIdAsync(int id);
        Task InserirAsync(MovieModel movie);
        Task AtualizarAsync(MovieModel movie);
        Task RemoverAsync(int id);
    }
}
