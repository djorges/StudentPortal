using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
