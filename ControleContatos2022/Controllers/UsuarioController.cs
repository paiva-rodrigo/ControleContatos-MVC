using ControleContatos2022.Filters;
using ControleContatos2022.Models;
using ControleContatos2022.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ControleContatos2022.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()//retorna a view criar
        {
            return View();
        }
        public IActionResult Editar(int id)//retorna a view criar
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)//Criar Contato
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "usuario cadastrado com sucesso";
                    return RedirectToAction("Index");//Redirecionando para a pagina index
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuario, tente denovo, detalhes: {erro.Message}";
                return RedirectToAction("Index");//Redirecionando para a pagina index
            }
        }

        public IActionResult ApagarConfirmacao(int id)//Deletar usuario
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {

            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                if (apagado) TempData["MensagemSucesso"] = "Usuario apagado com sucesso"; else TempData["MensagemErro"] = "Não conseguimos apagar o usuário";
                return RedirectToAction("Index");
            }
            catch(Exception erro)
            {
                TempData["MensagemErro"] = $"erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)//Criar usuario
        {
            try
            {
                UsuarioModel usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };

                    TempData["MensagemSucesso"] = "Usuario Atualizado com sucesso";
                    _usuarioRepositorio.Atualizar(usuario);
                    return RedirectToAction("Index");//Redirecionando para a pagina index
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu usuario, tente denovo, detalhes: {erro.Message}";
                return View("Index");//view que voce quer e o valor que voce quer editar
            }
        }

    }
}
