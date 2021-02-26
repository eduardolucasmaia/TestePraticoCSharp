using System;
using System.Collections.Generic;

namespace Dominio.Entities.DataModels
{
    public partial class VinculoUsuarioRole
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public Role IdRoleNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
    }
}
