using Dominio.Entities.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Interfaces
{
    public interface IUsuarioInfra
    {
        #region CONSULTAS
        Task<Usuario> AutenticarUsuario(string nomeUsuario, string senha);
        Task<List<Usuario>> BuscarUsuarios(int id);
        Task<Usuario> BuscarUsuarioPorId(int id);
        Task<Usuario> BuscarUsuarioPorEmail(string email);
        Task<string> BuscarFotoPerfilUsuarioAtual(int id);
        #endregion
        #region AÇÕES
        Task<bool> SalvarUsuario(Usuario usuario);
        Task<bool> ExcluirUsuario(int id);
        Task<Usuario> TrocarSenha(string senhaAntiga, string novaSenha, int id);
        #endregion
    }
}