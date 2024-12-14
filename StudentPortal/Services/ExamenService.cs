using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Entities;
using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class ExamenService
    {
        private readonly ILogger<ExamenService> _logger;
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExamenService(IHttpContextAccessor httpContextAccessor, DBMain dbMain, IMapper mapper, ILogger<ExamenService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbMain = dbMain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginacionResultado<Examen>> ObtenerExamenesPaginados(
            int pagina,
            string? busquedaNombre = null,
            string? sede = null,
            string? periodo = null,
            int? duracion = null,
            int cantidadPorPagina = 3
        )
        {
            var query = _dbMain.Examenes.AsQueryable();

            if (!string.IsNullOrEmpty(sede)) query = query.Where(c => c.Sede == sede);
            if (!string.IsNullOrEmpty(busquedaNombre)) query = query.Where(c => c.Titulo.Contains(busquedaNombre));
            if (!string.IsNullOrEmpty(periodo)) query = query.Where(c => c.PeriodoLectivo.Contains(periodo));
            if (duracion.HasValue) query = query.Where(c => c.DuracionHoras == duracion);

            var totalElementos = await query.CountAsync();
            var examenes = await query
                .Include(c => c.Profesor)
                .Include(c => c.FechasDisponibles)
                .Skip((pagina - 1) * cantidadPorPagina)
                .Take(cantidadPorPagina)
                .ToListAsync();

            return new PaginacionResultado<Examen>
            {
                Elementos = examenes,
                PaginaActual = pagina,
                TotalPaginas = (int)Math.Ceiling((double)totalElementos / cantidadPorPagina),
                TotalElementos = totalElementos,
                Filtros = new Dictionary<string, object?>
                {
                    { "BusquedaNombre", busquedaNombre != null ? Uri.EscapeDataString(busquedaNombre) : null},
                    { "Sede", sede != null ? Uri.EscapeDataString(sede) : null },
                    { "Periodo", periodo != null ? Uri.EscapeDataString(periodo) : null},
                    { "Duracion", duracion }
                }
            };

        }

        public async Task<ExamenEstudiante?> AsignarEstudianteAExamenAsync(int examenId, int fechaSeleccionada)
        {
            ExamenEstudiante? examen1 = null;
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("_UserId");
            if (userId != null)
            {
                try
                {
                    var examen = await _dbMain.Examenes.FindAsync(examenId);
                    var fechas = await _dbMain.FechaExamen.Where(fe => fe.ExamenId == examenId).ToListAsync();
                    var estudiante = await _dbMain.Estudiantes.FindAsync(userId);
                    var yaInscrito = await _dbMain.ExamenEstudiantes.AnyAsync(ce => ce.ExamenId == examenId && ce.EstudianteId == userId.Value);

                    if (examen != null && estudiante != null && !yaInscrito && fechas.Count > 0)
                    {
                        examen1 = new ExamenEstudiante
                        {
                            ExamenId = examenId,
                            EstudianteId = userId.Value,
                            FechaInscripcion = fechas[fechaSeleccionada].Fecha
                        };
                        await _dbMain.ExamenEstudiantes.AddAsync(examen1);
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
            return examen1;
        }

        public async Task<bool> DesasignarEstudianteDeExamenAsync(int examenId, int estudianteId)
        {
            try
            {
                var filasAfectadas = 0;
                var examenEstudiante = await _dbMain.ExamenEstudiantes.FirstOrDefaultAsync(ce => ce.ExamenId == examenId && ce.EstudianteId == estudianteId);

                if (examenEstudiante != null)
                {
                    _dbMain.ExamenEstudiantes.Remove(examenEstudiante);
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

        public async Task<List<Examen>?> ObtenerExamenesPorEstudianteIdAsync(int estudianteId)
        {
            List<Examen>? examenes = null;
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("_UserId");

            if (userId != null && userId.Value == estudianteId)
            {
                examenes = await _dbMain.ExamenEstudiantes
                .Where(ce => ce.EstudianteId == estudianteId)
                .Include(ce => ce.Examen)
                 .ThenInclude(c => c.Profesor)
                .Select(ce => ce.Examen)
                .ToListAsync();
            }

            return examenes;
        }
    }
}
