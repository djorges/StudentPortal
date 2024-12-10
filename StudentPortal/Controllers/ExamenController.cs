using Microsoft.AspNetCore.Mvc;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    public class ExamenController : Controller
    {
        private readonly ExamenService _examenService;
        const string SessionUserId = "_UserId";
        const string SessionUserName = "_UserName";

        public ExamenController(ExamenService examenService)
        {
            _examenService = examenService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
           string? busquedaNombre = null,
           int? pagina = 1
        )
        {
            /*var resultado = await _examenService.ObtenerCursosPaginados(
                pagina: pagina.Value,
                busquedaNombre: busquedaNombre
            );*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Inscribir(int cursoId)
        {
            return RedirectToAction("Listar");
        }
    }
}
