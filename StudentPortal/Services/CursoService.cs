using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Entities;
using StudentPortal.Models;
namespace StudentPortal.Services
{
    public class CursoService
    {
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;

        public CursoService(DBMain dbMain, IMapper mapper)
        {
            _dbMain = dbMain;
            _mapper = mapper;
        }

        public async Task<PaginacionResultado<Curso>> GetAllPaginated(
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

        public async Task<List<Curso>> GetAll() {
            return await _dbMain.Cursos.ToListAsync();
        }

        public async Task<List<Curso>> GetTopRated() { 
            return await _dbMain.Cursos
                .Include(c => c.Calificacion)
                .OrderByDescending(c => c.Calificacion.Puntuacion)
                .Take(3)
                .ToListAsync();
        }
    }
}
