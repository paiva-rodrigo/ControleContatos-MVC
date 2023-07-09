using System.ComponentModel.DataAnnotations;

namespace ControleContatos2022.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite o nome do contato")]//usamos isso para validar o campo
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o email do contato")]
        [EmailAddress(ErrorMessage = "O Email informado não é válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage ="O celular informado não é válido")]
        public string Celular { get; set; }
    }
}
