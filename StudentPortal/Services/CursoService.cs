using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentPortal.Data;
using StudentPortal.Entities;
using StudentPortal.Models;
namespace StudentPortal.Services
{
    public class CursoService
    {
        private readonly ILogger<CursoService> _logger;
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CursoService(IHttpContextAccessor httpContextAccessor, DBMain dbMain, IMapper mapper, ILogger<CursoService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbMain = dbMain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginacionResultado<Curso>> ObtenerCursosPaginados(
            int pagina,
            string? busquedaNombre = null,
            string? sede = null,
            int? creditos = null,
            bool? obligatorio = null,
            string? duracion = null,
            int cantidadPorPagina = 10
        ){
            var query = _dbMain.Cursos.AsQueryable();

            if (!string.IsNullOrEmpty(sede)) query = query.Where(c => c.Sede == sede);
            if (creditos.HasValue) query = query.Where(c => c.Creditos == creditos);
            if (!string.IsNullOrEmpty(busquedaNombre)) query = query.Where(c => c.Descripcion.Contains(busquedaNombre));
            if (!string.IsNullOrEmpty(duracion)) query = query.Where(c => c.Duracion == duracion);
            if (obligatorio.HasValue) query = query.Where(c => c.EsObligatorio == obligatorio.Value);

            var totalElementos = await query.CountAsync();
            var cursos = await query
                .Include(c => c.Profesor)
                .Skip((pagina - 1) * cantidadPorPagina)
                .Take(cantidadPorPagina)
                .ToListAsync();

            return new PaginacionResultado<Curso>
            {
                Elementos = cursos,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling((double)totalElementos / cantidadPorPagina),
                TotalElementos = totalElementos,
                Filtros = new FiltrosDto() { 
                    BusquedaNombre = busquedaNombre,
                    Sede = sede,
                    Creditos = creditos,
                    Obligatorio = obligatorio,
                    Duracion = duracion
                }
            };
        }

        public async Task<List<Curso>> ObtenerCursosAsync() {
            return await _dbMain.Cursos.ToListAsync();
        }

        public async Task<List<Curso>> ObtenerCursosMejorPuntuadosAsync() { 
            return await _dbMain.Cursos
                .Include(c => c.Calificacion)
                .OrderByDescending(c => c.Calificacion.Puntuacion)
                .Take(3)
                .ToListAsync();
        }

        public async Task<CursoEstudiante?> AsignarEstudianteACursoAsync(int cursoId)
        {
            CursoEstudiante? curso1 = null;
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("_UserId");
            if (userId != null) {
                try
                {
                    var curso = await _dbMain.Cursos.FindAsync(cursoId);
                    var estudiante = await _dbMain.Estudiantes.FindAsync(userId);
                    var yaInscrito = await _dbMain.CursoEstudiantes.AnyAsync(ce => ce.CursoId == cursoId && ce.EstudianteId == userId.Value);

                    if (curso != null && estudiante != null && !yaInscrito)
                    {
                        curso1 = new CursoEstudiante
                        {
                            CursoId = cursoId,
                            EstudianteId = userId.Value,
                            FechaInscripcion = DateTime.Now
                        };
                        await _dbMain.CursoEstudiantes.AddAsync(curso1);
                        await _dbMain.SaveChangesAsync();
                    }
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError(dbEx, $"Error en la base de datos: {dbEx.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error general: {ex.Message}");
                }
            }
            return curso1;
        }

        public async Task<bool> DesasignarEstudianteDeCursoAsync(int cursoId, int estudianteId)
        {
            try
            {
                var filasAfectadas = 0;
                var cursoEstudiante = await _dbMain.Set<CursoEstudiante>()
                .FirstOrDefaultAsync(ce => ce.CursoId == cursoId && ce.EstudianteId == estudianteId);

                if (cursoEstudiante != null)
                {
                    _dbMain.Set<CursoEstudiante>().Remove(cursoEstudiante);
                    filasAfectadas = await _dbMain.SaveChangesAsync();
                }
                return filasAfectadas > 0;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, $"Error en la base de datos: {dbEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error general: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Curso>?> ObtenerCursosPorEstudianteIdAsync(int estudianteId)
        {
            List<Curso>? cursos = null;
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("_UserId");
            
            if (userId != null && userId.Value == estudianteId) {
                cursos = await _dbMain.CursoEstudiantes
                .Where(ce => ce.EstudianteId == estudianteId)
                .Include(ce => ce.Curso)
                 .ThenInclude(c => c.Profesor)
                .Select(ce => ce.Curso)
                .ToListAsync();
            }
           
            return cursos;
        }
    }
}
