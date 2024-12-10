using AutoMapper;
using MailKit;
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
        private readonly UtilService _utilService;

        public EstudianteService(UtilService utilService, DBMain dbMain, IMapper mapper, ILogger<EstudianteService> logger)
        {
            _utilService = utilService;
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

        public async Task<EstudianteDto?> ValidarAsync(string correo, string clave)
        {
            EstudianteDto? usuario = null;

            try
            {
                var claveEncrypt = _utilService.ConvertirSHA256(clave);
                var usuarioEntidad = await _dbMain.Estudiantes
                    .Where(u => u.Correo == correo && u.Clave == claveEncrypt)
                    .FirstOrDefaultAsync();

                if (usuarioEntidad != null)
                {
                    var token = _utilService.GenerarToken();
                    usuarioEntidad.Token = token;
                    await _dbMain.SaveChangesAsync();

                    usuario = new EstudianteDto
                    {
                        EstudianteId = usuarioEntidad.EstudianteId,
                        Username = usuarioEntidad.Username,
                        Nombre = usuarioEntidad.Nombre,
                        Restablecer = usuarioEntidad.Restablecer,
                        Confirmado = usuarioEntidad.Confirmado,
                        Token = token
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar usuario: {ex.Message}");
            }

            return usuario;
        }

        public async Task<EstudianteDto?> ObtenerPorCorreoAsync(string correo)
        {
            EstudianteDto? usuario = null;

            try
            {
                var usuarioEntidad = await _dbMain.Estudiantes
                    .Where(u => u.Correo == correo)
                    .FirstOrDefaultAsync();

                if (usuarioEntidad != null)
                {
                    usuario = new EstudianteDto
                    {
                        Nombre = usuarioEntidad.Nombre,
                        Clave = usuarioEntidad.Clave,
                        Restablecer = usuarioEntidad.Restablecer,
                        Confirmado = usuarioEntidad.Confirmado,
                        Token = usuarioEntidad.Token
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener usuario: {ex.Message}");
            }

            return usuario;
        }

        public async Task<EstudianteDto?> ObtenerPorIdAsync(int id) {

            EstudianteDto? usuario = null;

            try
            {
                var usuarioEntidad = await _dbMain.Estudiantes
                    .Where(u => u.EstudianteId == id)
                    .FirstOrDefaultAsync();

                if (usuarioEntidad != null)
                {
                    usuario = new EstudianteDto
                    {
                        Nombre = usuarioEntidad.Nombre,
                        Apellido = usuarioEntidad.Apellido,
                        Username = usuarioEntidad.Username,
                        FechaNacimiento = usuarioEntidad.FechaNacimiento,
                        Dni = usuarioEntidad.Dni,
                        Nacionalidad = usuarioEntidad.Nacionalidad,
                        Telefono = usuarioEntidad.Telefono,
                        Correo = usuarioEntidad.Correo,
                        PrivacidadCorreo = usuarioEntidad.PrivacidadCorreo,
                        Genero = usuarioEntidad.Genero,
                        Domicilio = usuarioEntidad.Domicilio,
                        Descripcion = usuarioEntidad.Descripcion,
                        FotoPerfilBytes = usuarioEntidad.FotoPerfil,

                        Clave = usuarioEntidad.Clave,
                        Restablecer = usuarioEntidad.Restablecer,
                        Confirmado = usuarioEntidad.Confirmado,
                        Token = usuarioEntidad.Token
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener usuario: {ex.Message}");
            }

            return usuario;
        }

        public async Task<bool> ActualizarAsync(int id, EstudianteDto estudianteDto) {
            try
            {

                // Foto a byte
                byte[]? fotoBytes = null;

                if (estudianteDto.FotoPerfil != null && estudianteDto.FotoPerfil.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await estudianteDto.FotoPerfil.CopyToAsync(memoryStream);
                    fotoBytes = memoryStream.ToArray();
                }
                
                var result = _dbMain.Estudiantes
                    //.Where(user => user.Token == estudianteDto.Token && user.Username == estudianteDto.Username)
                    .Where(user => user.EstudianteId == id)
                    .FirstOrDefault();

                if (result != null)
                {
                    result.FotoPerfil = fotoBytes;
                    result.Descripcion = estudianteDto.Descripcion?.ToString();
                }

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

        public async Task<bool> RestablecerClaveAsync(int restablecer, string clave, string token)
        {
            bool respuesta = false;

            try
            {
                var usuarioEntidad = await _dbMain.Estudiantes
                    .Where(u => u.Token == token)
                    .FirstOrDefaultAsync();

                if (usuarioEntidad != null)
                {
                    usuarioEntidad.Restablecer = restablecer == 1;  
                    usuarioEntidad.Clave = clave;

                    int filasAfectadas = await _dbMain.SaveChangesAsync();
                    if (filasAfectadas > 0)
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el restablecimiento de contraseña: {ex.Message}");
            }

            return respuesta;
        }

        public async Task<bool> ConfirmarAsync(string token)
        {
            bool respuesta = false;

            try
            {
                var usuarioEntidad = await _dbMain.Estudiantes
                    .Where(u => u.Token == token)
                    .FirstOrDefaultAsync();

                if (usuarioEntidad != null)
                {
                    usuarioEntidad.Confirmado = true;

                    int filasAfectadas = await _dbMain.SaveChangesAsync();
                    if (filasAfectadas > 0)
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al confirmar el token: {ex.Message}");
            }

            return respuesta;
        }
    }
}
