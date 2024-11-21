using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class EstudianteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
