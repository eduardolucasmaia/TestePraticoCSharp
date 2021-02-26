using DTO.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IJogoDominio
    {
        #region AÇÕES
        Task<bool> SalvarJogo(JogoDTO jogo);
        Task<bool> ExcluirJogo(int id);
        #endregion

        #region CONSULTAS
        Task<List<JogoDTO>> BuscarJogosDisponiveis(int id);
        Task<List<JogoDTO>> BuscarJogos();
        Task<List<JogoDTO>> BuscarJogoPorNome(string nome);
        Task<JogoDTO> BuscarJogoPorId(int id);
        #endregion
    }
}
