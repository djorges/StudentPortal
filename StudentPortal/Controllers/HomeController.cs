using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using StudentPortal.Services;
using System.Diagnostics;

namespace StudentPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProfesorService _profesorService;
        private readonly CursoService _cursoService;

        public HomeController(ILogger<HomeController> logger, ProfesorService profesorService, CursoService cursoService)
        {
            _logger = logger;
            _profesorService = profesorService;
            _cursoService = cursoService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["TopProfesores"] = await _profesorService.GetTopRated();
            ViewData["TopCursos"] = await _cursoService.ObtenerCursosMejorPuntuadosAsync();

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
