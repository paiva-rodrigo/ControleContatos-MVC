using ControleContatos2022.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos2022.Models
{
    public class UsuarioModel
    {
       public int  Id { get; set; }

       [Required(ErrorMessage = "Digite o nome do usuario")]//usamos isso para validar o campo
       public string Nome { get; set; }

       [Required(ErrorMessage = "Digite o login do usuario")]//usamos isso para validar o campo
       public string Login { get; set; }

       [Required(ErrorMessage = "Digite o Email do usuario")]
       [EmailAddress(ErrorMessage = "O Email informado não é válido")]
       public string Email { get; set; }

       [Required(ErrorMessage = "Informe o perfil do usuario")]
       public PerfilEnum? Perfil { get; set; }

       [EmailAddress(ErrorMessage = "Digite a senha do usuário")]
       public string Senha { get; set; }
       public DateTime DataCadastro { get; set; }
       public DateTime? DataAtualizacao { get; set; }


      public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
