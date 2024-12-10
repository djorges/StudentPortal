using AutoMapper;
using StudentPortal.Data;
using StudentPortal.Entities;
using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class ExamenService
    {
        private readonly ILogger<CursoService> _logger;
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExamenService(IHttpContextAccessor httpContextAccessor, DBMain dbMain, IMapper mapper, ILogger<CursoService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbMain = dbMain;
            _mapper = mapper;
            _logger = logger;
        }

        /*public async Task<PaginacionResultado<Examen>> ObtenerCursosPaginados(
           int pagina,
           string? busquedaNombre = null,
           int cantidadPorPagina = 10
       )
        { 
        } */
    }
}
