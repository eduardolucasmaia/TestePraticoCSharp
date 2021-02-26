using DTO.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUsuarioDominio
    {
        #region CONSULTAS
        Task<UsuarioDTO> AutenticarUsuario(string nomeUsuario, string senha);
        Task<List<UsuarioDTO>> BuscarUsuarios(int id);
        Task<UsuarioDTO> BuscarUsuarioPorId(int id);
        Task<UsuarioDTO> BuscarUsuarioPorEmail(string email);
        Task<string> BuscarFotoPerfilUsuarioAtual(int id);
        Task<UsuarioDTO> BuscarUsuarioAtual(int id);
        #endregion
        #region AÇÕES
        Task<bool> SalvarUsuario(UsuarioDTO usuario);
        Task<bool> AtualizarPerfil(UsuarioDTO usuario, int id);
        Task<bool> ExcluirUsuario(int id);
        Task<UsuarioDTO> TrocarSenha(string senhaAntiga, string novaSenha, int id);
        #endregion
    }
}