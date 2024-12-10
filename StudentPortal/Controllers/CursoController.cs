using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using StudentPortal.Entities;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    public class CursoController : Controller
    {
        private readonly CursoService _cursoService;
        const string SessionUserId = "_UserId";
        const string SessionUserName = "_UserName";

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
        ) {
            var resultado = await _cursoService.ObtenerCursosPaginados(
                pagina: pagina.Value,
                busquedaNombre: busquedaNombre,
                sede: sede,
                creditos: creditos,
                obligatorio: obligatorio,
                duracion: duracion
            );
            return View(resultado);
        }

        [HttpGet("Curso/Estudiante/{estudianteId}")]
        public async Task<IActionResult> ListarCursosPorEstudianteId(int estudianteId) {

            var cursos = await _cursoService.ObtenerCursosPorEstudianteIdAsync(estudianteId);
            
            if (cursos == null) { 
                return RedirectToAction("Index","Home");
            }

            ViewBag.UserId = estudianteId;
            return View("CursosPorEstudiante", cursos);
        }

        [HttpPost]
        public async Task<IActionResult> Inscribir(int cursoId) {
            var resultado = await _cursoService.AsignarEstudianteACursoAsync(cursoId);
            if (resultado == null) {
                ViewBag.Mensaje = "Error al inscribirse";
                return RedirectToAction("Listar");
            }

            ViewBag.Mensaje = "Se inscribió correctamente";
            return RedirectToAction("ListarCursosPorEstudianteId", new { estudianteId = resultado.EstudianteId });
        }

        [HttpPost]
        public async Task<IActionResult> Desinscribir(int cursoId, int estudianteId) {
            
            var resultado = await _cursoService.DesasignarEstudianteDeCursoAsync(cursoId, estudianteId);
            if (!resultado) {

                ViewBag.Mensaje = "Error al desinscribirse";
                return RedirectToAction("Listar");
            }

            ViewBag.Mensaje = "Se desinscribió correctamente";
            return RedirectToAction("ListarCursosPorEstudianteId", new { estudianteId = estudianteId });
        }
    }
}
