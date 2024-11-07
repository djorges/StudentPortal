using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    [ApiController]
    [Route("api/v1/empleado")]
    //[TypeFilter(typeof(ApiExceptionFilter))]
    public class EmpleadoRestController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadoRestController(EmpleadoService empleadoService) {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<ObjectResult> Listar() {
            return Ok(await _empleadoService.Listar());
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ObjectResult> ObtenerPorId(int id) {
            var resultado = await _empleadoService.ObtenerPorId(id);

            if (resultado == null)
            {
                return NotFound($"No se encontró al empleado con id = {id}");
            }

            return Ok(resultado);
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<ObjectResult> Guardar([FromBody] EmpleadoDto empleadoDto) {
            var resultado = await _empleadoService.Guardar(empleadoDto);

            if (resultado == null)
            {
                return StatusCode(500, "Hubo un error al guardar el empleado.");
            }

            return Ok("Empleado Guardado");
        }

        [HttpPut]
        [Route("editar")]
        public async Task<ObjectResult> Editar([FromBody] EmpleadoDto empleadoDto) {
            var resultado = await _empleadoService.Editar(empleadoDto);

            if (resultado == null)
            {
                return NotFound($"No se encontró al empleado con id = {empleadoDto.Id}");
            }

            return Ok("Empleado Editado");
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ObjectResult> Eliminar(int id)
        {
            var resultado = await _empleadoService.Eliminar(id);

            if (resultado == null)
            {
                NotFound($"No se encontró al empleado con id = {id}");
            }

            return Ok("Empleado Eliminado");
        }
    }
}
