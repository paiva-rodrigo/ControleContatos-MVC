using ControleContatos2022.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos2022.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
