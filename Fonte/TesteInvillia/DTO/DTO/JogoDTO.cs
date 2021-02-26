using DTO.Ferramentas;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class JogoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Usuário locatário")]
        public int? IdUsuario { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = Mensagens.MS_005)]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(200, ErrorMessage = Mensagens.MS_004)]
        public string Descricao { get; set; }

        [Display(Name = "Plataforma")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int Plataforma { get; set; }

        [Display(Name = "Observações")]
        [MaxLength(200, ErrorMessage = Mensagens.MS_004)]
        public string Observacao { get; set; }

        [Display(Name = "Imagem")]
        public string FotoBase64 { get; set; }
        public string MiniaturaBase64 { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Excluido { get; set; }

        public UsuarioDTO IdUsuarioNavigation { get; set; }

        #region AUXILIAR

        [Display(Name = "Data cadastro")]
        public string DataCadastroFormatado => DataCadastro.ToString("dd/MM/yyyy HH:mm");

        [Display(Name = "Data alteração")]
        public string DataAlteracaoFormatado => DataAlteracao?.ToString("dd/MM/yyyy HH:mm");

        [Display(Name = "Usuário utilizando")]
        public string UsuarioLocatario
        {
            get
            {
                return IdUsuarioNavigation != null ? IdUsuarioNavigation.Nome : string.Empty;
            }
        }

        [Display(Name = "Emprestado")]
        public bool Emprestado
        {
            get
            {
                return IdUsuarioNavigation != null;
            }
        }

        [Display(Name = "Plataforma")]
        public string PlataformaFormatado
        {
            get
            {
                try
                {
                    return Constantes.PLATAFORMAS.Find(x => x.Id == Plataforma).Nome;
                }
                catch
                {
                    return Constantes.SEM_PLATAFORMA;
                }
                       
            }
        }

        #endregion
    }
}