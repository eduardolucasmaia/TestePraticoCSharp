using DTO.Ferramentas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DTO.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public int? IdCriadoUsuario { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = Mensagens.MS_005)]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = Mensagens.MS_005)]
        [MaxLength(200, ErrorMessage = Mensagens.MS_004)]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = Mensagens.MS_009)]
        public string Email { get; set; }

        [Display(Name = "CPF")]
        [MaxLength(14, ErrorMessage = Mensagens.MS_004)]
        public string Cpf { get; set; }

        [Display(Name = "Senha")]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        [MinLength(6, ErrorMessage = Mensagens.MS_005)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(15, ErrorMessage = Mensagens.MS_004)]
        public string Telefone { get; set; }

        [Display(Name = "Observações")]
        [MaxLength(200, ErrorMessage = Mensagens.MS_004)]
        public string Observacao { get; set; }

        [Display(Name = "Foto")]
        public string FotoBase64 { get; set; }
        public string MiniaturaBase64 { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public UsuarioDTO IdCriadoUsuarioNavigation { get; set; }
        public ICollection<UsuarioDTO> InverseIdCriadoUsuarioNavigation { get; set; }
        public ICollection<JogoDTO> Jogo { get; set; }
        public ICollection<VinculoUsuarioRoleDTO> VinculoUsuarioRole { get; set; }

        #region AUXILIAR

        [Display(Name = "Jogos selecionados")]
        public List<int> JogosSelecionados { get; set; }

        [Display(Name = "Data cadastro")]
        public string DataCadastroFormatado => DataCadastro.ToString("dd/MM/yyyy HH:mm");

        [Display(Name = "Data alteração")]
        public string DataAlteracaoFormatado => DataAlteracao?.ToString("dd/MM/yyyy HH:mm");

        [Display(Name = "Permissões")]
        public string VinculoUsuarioRoleFormatado
        {
            get
            {
                var roles = string.Empty;
                if (VinculoUsuarioRole != null)
                {
                    foreach (var loop in VinculoUsuarioRole.Where(x => !x.Excluido).ToList())
                    {
                        if (loop.IdRoleNavigation != null)
                        {
                            roles += (string.IsNullOrEmpty(roles) ? string.Empty : ", ") + loop.IdRoleNavigation.NomeRole;
                        }
                    }
                }
                return roles;
            }
        }

        [Display(Name = "Jogos emprestados")]
        public string JogosEmprestados
        {
            get
            {
                var jogos = string.Empty;
                if (Jogo != null)
                {
                    foreach (var loop in Jogo.Where(x => !x.Excluido).ToList())
                    {
                        jogos += (string.IsNullOrEmpty(jogos) ? string.Empty : ", ") + loop.Nome;
                    }
                }
                return jogos;
            }
        }

        #endregion

    }
}