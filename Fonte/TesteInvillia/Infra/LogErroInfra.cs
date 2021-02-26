using Dominio.Entities.DataModels;
using Infra.Interfaces;
using System.Threading.Tasks;

namespace Infra
{
    public class LogErroInfra : ILogErroInfra
    {
        #region AÇÕES
        public async Task SalvarLog(LogErro logErro)
        {
            using (var db = new TesteInvilliaContext())
            {
                await db.LogErro.AddAsync(logErro);
                await db.SaveChangesAsync();
            }
        }
        #endregion
    }
}