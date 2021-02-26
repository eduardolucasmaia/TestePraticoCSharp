using DTO.Ferramentas;
using System.ComponentModel.DataAnnotations;

namespace TesteInvillia.Models
{
    public class EsqueceuSenhaViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = Mensagens.MS_005)]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Sucesso { get; set; }

    }
}
