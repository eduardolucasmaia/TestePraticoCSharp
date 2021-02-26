using System;

namespace DTO.DTO
{
    public class VinculoUsuarioRoleDTO
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public RoleDTO IdRoleNavigation { get; set; }
        public UsuarioDTO IdUsuarioNavigation { get; set; }
    }
}
