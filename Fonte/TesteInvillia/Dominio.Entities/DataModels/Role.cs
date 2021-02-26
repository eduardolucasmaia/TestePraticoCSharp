using System;
using System.Collections.Generic;

namespace Dominio.Entities.DataModels
{
    public partial class Role
    {
        public Role()
        {
            VinculoUsuarioRole = new HashSet<VinculoUsuarioRole>();
        }

        public int Id { get; set; }
        public string NomeRole { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public ICollection<VinculoUsuarioRole> VinculoUsuarioRole { get; set; }
    }
}
