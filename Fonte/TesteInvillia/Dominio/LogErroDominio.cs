using Dominio.Entities.DataModels;
using Dominio.Interfaces;
using Infra.Interfaces;
using System;
using System.Threading.Tasks;

namespace Dominio
{
    public class LogErroDominio : ILogErroDominio
    {
        #region Construtor

        private readonly ILogErroInfra _logErroInfra;

        public LogErroDominio(ILogErroInfra logErroInfra)
        {
            _logErroInfra = logErroInfra;
        }

        #endregion

        #region AÇÕES
        public async Task SalvarLog(Exception ex)
        {
            try
            {
                await _logErroInfra.SalvarLog(new LogErro()
                {
                    DataCadastro = DateTime.Now,
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException != null ? 
                    string.Concat("Message:", ex.InnerException.Message ?? string.Empty, "||Source:", ex.InnerException.Source ?? string.Empty, "||StackTrace:", ex.InnerException.StackTrace ?? string.Empty) : null
                });
            }
            catch { }
        }
        #endregion
    }
}