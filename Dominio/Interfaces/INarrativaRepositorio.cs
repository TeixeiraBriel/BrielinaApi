using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface INarrativaRepositorio
    {
        Task<List<Narrativa>> ObterTodasNarrativas();
        Task<List<Narrativa>> ObterTodasNarrativasFilhas(string idPai);
        Task<Narrativa> ObterNarrativaPorId(int Id);
        Task CriarNarrativa(Narrativa newNarrativa);
        Task DeleteNarrativa(int Id);
        Task EditNarrativa(Narrativa narrativaAtualizada);
    }
}
