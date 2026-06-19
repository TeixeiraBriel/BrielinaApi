using Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IMovieReviewRepositorio
    {
        Task<List<MovieReviewModel>> ListarPorFilmeAsync(int movieId);
        Task<List<MovieReviewModel>> ListarPorUsuarioAsync(long usuarioId);
        Task<MovieReviewModel?> ObterPorIdAsync(long id);
        Task InserirAsync(MovieReviewModel review);
        Task AtualizarAsync(MovieReviewModel review);
        Task RemoverAsync(long id);
        Task<double> ObterMediaRatingAsync(int movieId);
        Task<MovieReviewModel> UpsertAsync(MovieReviewModel review);
    }
}
