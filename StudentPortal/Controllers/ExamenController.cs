using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class ExamenController : Controller
    {
        public IActionResult Listar()
        {
            return View();
        }
    }
}
