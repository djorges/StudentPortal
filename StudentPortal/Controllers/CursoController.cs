using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class CursoController : Controller
    {
        [HttpGet]
        public IActionResult Listar()
        {
            return View();
        }
    }
}
