using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Entities;
using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class EstudianteService
    {
        private readonly ILogger<EstudianteService> _logger;
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;

        public EstudianteService(DBMain dbMain, IMapper mapper, ILogger<EstudianteService> logger)
        {
            _dbMain = dbMain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> RegistrarAsync(EstudianteDto estudianteDto)
        {
            try
            {
                var usuario = new Estudiante
                {
                    Nombre = estudianteDto.Nombre,
                    Correo = estudianteDto.Correo,
                    Clave = estudianteDto.Clave,
                    Restablecer = estudianteDto.Restablecer,
                    Confirmado = estudianteDto.Confirmado,
                    Token = estudianteDto.Token
                };

                _dbMain.Estudiantes.Add(usuario);

                var filasAfectadas = await _dbMain.SaveChangesAsync();

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
    }
}
