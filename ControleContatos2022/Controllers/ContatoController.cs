using ControleContatos2022.Filters;
using ControleContatos2022.Models;
using ControleContatos2022.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos2022.Controllers
{
    [PaginaParaUsuarioLogado]//ele precisa esta logado para ter acess
    public class ContatoController : Controller
    {

        private readonly IContatoRepositorio _contatoRepositorio;                                                                                     
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()//metod da pagina index daquela controller
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()//criar contato
        {
            return View();
        }

        public IActionResult Editar(int Id)//Editar Contato
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(Id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)//Deletar Contato
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            _contatoRepositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)//Criar Contato
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");//Redirecionando para a pagina index
                }
                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemError"] = $"Ops, não conseguimos cadastrar seu contato, tente denovo, detalhes: {erro.Message}";
                return RedirectToAction("Index");//Redirecionando para a pagina index
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)//Criar Contato
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["MensagemSucesso"] = "Atualizado com sucesso";
                    _contatoRepositorio.Atualizar(contato);
                    return RedirectToAction("Index");//Redirecionando para a pagina index
                }
                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu contato, tente denovo, detalhes: {erro.Message}";
                return View("Editar", contato);//view que voce quer e o valor que voce quer editar
            }
        }

    }
}
