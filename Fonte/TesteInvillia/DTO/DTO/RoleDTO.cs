using System;
using System.Collections.Generic;

namespace DTO.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string NomeRole { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public ICollection<VinculoUsuarioRoleDTO> VinculoUsuarioRole { get; set; }
    }
}
