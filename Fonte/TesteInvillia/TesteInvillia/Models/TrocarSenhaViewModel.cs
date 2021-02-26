using DTO.Ferramentas;
using System.ComponentModel.DataAnnotations;

namespace TesteInvillia.Models
{
    public class TrocarSenhaViewModel
    {
        [Required(ErrorMessage = Mensagens.MS_005)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha antiga")]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        [MinLength(5, ErrorMessage = Mensagens.MS_005)]
        public string SenhaAntiga { get; set; }

        [Required(ErrorMessage = Mensagens.MS_005)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        [MinLength(5, ErrorMessage = Mensagens.MS_005)]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = Mensagens.MS_005)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha (Confirmar)")]
        [MaxLength(100, ErrorMessage = Mensagens.MS_004)]
        [MinLength(5, ErrorMessage = Mensagens.MS_005)]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não coincidem.")]
        public string NovaSenhaConfirmar { get; set; }

        public bool Sucesso { get; set; }

    }
}