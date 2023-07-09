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
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                                ISessao sessao,
                                IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email =  email;
        }

        public IActionResult Index()
        {
            // se o usuario estiver logado redirecionar para a home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
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

        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é:{novaSenha}";

                        bool emailEviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", mensagem);
                        if (emailEviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Email enviado com uma nova senha";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguirmos enviar o email, verifique os seus dados";
                        }
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não conseguirmos redefinir sua senha, verifique os seus dados";
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, tente denovo, detalhes: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


    }
}
