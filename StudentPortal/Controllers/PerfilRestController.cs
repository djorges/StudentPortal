using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    [ApiController]
    [Route("api/v1/perfil")]
    public class PerfilRestController : ControllerBase
    {

        private readonly PerfilService _perfilService;

        public PerfilRestController(PerfilService perfilService) {
            _perfilService = perfilService;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<List<PerfilDto>>> GetAll()
        {
            return Ok(await _perfilService.GetAll());
        }
    }
}
