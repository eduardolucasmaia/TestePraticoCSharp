using Dominio.Entities.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IJogoInfra
    {
        #region AÇÕES
        Task<bool> SalvarJogo(Jogo jogo);
        Task<bool> ExcluirJogo(int id);
        #endregion

        #region CONSULTAS
        Task<List<Jogo>> BuscarJogosDisponiveis(int id);
        Task<List<Jogo>> BuscarJogos();
        Task<List<Jogo>> BuscarJogosIn(List<int> ids, int id);
        Task<List<Jogo>> BuscarJogoPorNome(string nome);
        Task<Jogo> BuscarJogoPorId(int id);
        #endregion
    }
}