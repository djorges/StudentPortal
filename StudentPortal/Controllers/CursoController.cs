using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    public class CursoController : Controller
    {
        private readonly CursoService _cursoService;

        public CursoController(CursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            string? busquedaNombre = null,
            string? sede = null,
            int? creditos = null,
            bool? obligatorio = null,
            string? duracion = null,
            int? pagina = 1 
        ){
            var resultado = await _cursoService.GetAllPaginated(
                pagina: pagina.Value,
                busquedaNombre: busquedaNombre,
                sede: sede,
                creditos: creditos,
                obligatorio: obligatorio,
                duracion: duracion
            );
            return View(resultado);
        }
    }
}
