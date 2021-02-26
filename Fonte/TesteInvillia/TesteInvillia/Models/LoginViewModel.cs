using DTO.Ferramentas;
using System.ComponentModel.DataAnnotations;

namespace TesteInvillia.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Mensagens.MS_005)]
        [Display(Name = "E-mail ou telefone")]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        public string UserName { get; set; }

        [Required(ErrorMessage = Mensagens.MS_005)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        [MinLength(5, ErrorMessage = Mensagens.MS_005)]
        public string Senha { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool LembrarMe { get; set; }
    }
}
