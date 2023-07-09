namespace ControleContatos2022.Helper
{
    public interface IEmail
    {
        bool Enviar(string email ,string assuntoEmail, string mensagem);

    }
}
