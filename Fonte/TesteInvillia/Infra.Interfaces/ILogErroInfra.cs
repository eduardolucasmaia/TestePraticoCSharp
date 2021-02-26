using Dominio.Entities.DataModels;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface ILogErroInfra
    {
        #region AÇÕES
        Task SalvarLog(LogErro logErro);
        #endregion
    }
}