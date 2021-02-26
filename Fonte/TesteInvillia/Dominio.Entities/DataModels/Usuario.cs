using System;
using System.Collections.Generic;

namespace Dominio.Entities.DataModels
{
    public partial class Usuario
    {
        public Usuario()
        {
            InverseIdCriadoUsuarioNavigation = new HashSet<Usuario>();
            Jogo = new HashSet<Jogo>();
            VinculoUsuarioRole = new HashSet<VinculoUsuarioRole>();
        }

        public int Id { get; set; }
        public int? IdCriadoUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Observacao { get; set; }
        public string FotoBase64 { get; set; }
        public string MiniaturaBase64 { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public Usuario IdCriadoUsuarioNavigation { get; set; }
        public ICollection<Usuario> InverseIdCriadoUsuarioNavigation { get; set; }
        public ICollection<Jogo> Jogo { get; set; }
        public ICollection<VinculoUsuarioRole> VinculoUsuarioRole { get; set; }
    }
}
