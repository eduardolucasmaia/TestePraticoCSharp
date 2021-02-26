using System;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface ILogErroDominio
    {
        #region AÇÕES
        Task SalvarLog(Exception ex);
        #endregion
    }
}