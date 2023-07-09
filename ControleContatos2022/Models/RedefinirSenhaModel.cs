using System.ComponentModel.DataAnnotations;

namespace ControleContatos2022.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Digite ao email")]
        public string Email { get; set; }
    }
}
