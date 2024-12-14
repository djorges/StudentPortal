using Google.Protobuf.WellKnownTypes;
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
           string? sede = null,
           string? periodo = null,
           int? duracion = null,
           int? pagina = 1
        )
        {
            var resultado = await _examenService.ObtenerExamenesPaginados(
                pagina: pagina.Value,
                busquedaNombre: busquedaNombre,
                sede: sede,
                periodo: periodo,
                duracion: duracion
            );
            
            return View(resultado);
        }

        [HttpGet("Examen/Estudiante/{estudianteId}")]
        public async Task<IActionResult> ListarExamenPorEstudianteId(int estudianteId)
        {

            var examen = await _examenService.ObtenerExamenesPorEstudianteIdAsync(estudianteId);

            if (examen == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UserId = estudianteId;
            return View("ExamenesPorEstudiante", examen);
        }

        [HttpPost]
        public async Task<IActionResult> Inscribir(int examenId, int fechaSeleccionada)
        {
            var resultado = await _examenService.AsignarEstudianteAExamenAsync(examenId, fechaSeleccionada);
            if (resultado == null)
            {
                ViewBag.Mensaje = "Error al inscribirse";
                return RedirectToAction("Listar");
            }

            ViewBag.Mensaje = "Se inscribió correctamente";
            return RedirectToAction("ListarExamenPorEstudianteId", new { estudianteId = resultado.EstudianteId });
        }

        [HttpPost]
        public async Task<IActionResult> Desinscribir(int examenId, int estudianteId)
        {

            var resultado = await _examenService.DesasignarEstudianteDeExamenAsync(examenId, estudianteId);
            if (!resultado)
            {

                ViewBag.Mensaje = "Error al desinscribirse";
                return RedirectToAction("Listar");
            }

            ViewBag.Mensaje = "Se desinscribió correctamente";
            return RedirectToAction("ListarExamenPorEstudianteId", new { estudianteId = estudianteId });
        }
    }
}
