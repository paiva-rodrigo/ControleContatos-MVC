using ControleContatos2022.Models;

namespace ControleContatos2022.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();

        UsuarioModel BuscarSessaoDoUsuario();

    }
}
