using System;
using System.Collections.Generic;

namespace Dominio.Entities.DataModels
{
    public partial class Jogo
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Plataforma { get; set; }
        public string Observacao { get; set; }
        public string FotoBase64 { get; set; }
        public string MiniaturaBase64 { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
    }
}
