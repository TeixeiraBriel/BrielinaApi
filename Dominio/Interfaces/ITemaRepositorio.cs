using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ITemaRepositorio
    {
        /// <summary>
        /// Lista todos os temas com responsáveis, criadores e comentários carregados
        /// </summary>
        Task<List<TemaModel>> ListarAsync();

        /// <summary>
        /// Cria um novo tema
        /// </summary>
        Task InserirAsync(TemaModel tema);

        /// <summary>
        /// Atualiza um tema existente
        /// </summary>
        Task AtualizarAsync(TemaModel tema);

        /// <summary>
        /// Define o usuário logado como responsável pelo tema
        /// </summary>
        Task DefinirResponsavelAsync(int temaId, int usuarioId);

        /// <summary>
        /// Define a data de apresentação do tema
        /// </summary>
        Task DefinirDataApresentacaoAsync(int temaId, DateTime data);

        /// <summary>
        /// Obtém um tema específico com todos os dados relacionados
        /// </summary>
        Task<TemaModel?> ObterPorIdAsync(int id);

        /// <summary>
        /// Remove um tema (cascate remove os comentários)
        /// </summary>
        Task RemoverAsync(int id);
    }

}
