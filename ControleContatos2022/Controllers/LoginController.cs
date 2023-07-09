using ControleContatos2022.Helper;
using ControleContatos2022.Models;
using ControleContatos2022.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;

namespace ControleContatos2022.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                                ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            // se o usuario estiver logado redirecionar para a home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();

            return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try {

                UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                if (ModelState.IsValid)
                {
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Senha inválida. Tente novamente.";
                        }
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválidos. Tente novamente.";
                }
                return View("Index");
            }
            catch (Exception ex) {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente denovo, detalhes: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
