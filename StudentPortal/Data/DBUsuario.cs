using Microsoft.Extensions.Logging;
using StudentPortal.Models;
using StudentPortal.Services;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace StudentPortal.Data{

    /// <summary>
    /// Clase que contiene métodos estáticos para el registro y manipulación de usuarios en la base de datos.
    /// </summary>
    public class DBUsuario{

        private readonly string _CadenaSQL;
        private readonly ILogger<EmailService> _logger;

        public DBUsuario(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _CadenaSQL = configuration.GetConnectionString("LocalDBConnection");
            _logger = logger;
        }
        /// <summary>
        /// Registra un nuevo usuario en la base de datos con los datos proporcionados en el objeto `UsuarioDto`.
        /// </summary>
        /// <param name="usuario">Objeto `UsuarioDto` que contiene la información del usuario a registrar.</param>
        /// <returns>Devuelve `true` si el usuario fue registrado exitosamente; de lo contrario, `false`.</returns>
        public bool Registrar(UsuarioDto usuario) { 
            bool respuesta = false;

            try{
                using (SqlConnection connection = new SqlConnection(_CadenaSQL))
                {
                    string query = "insert into usuarios(Nombre, Correo, Clave, Restablecer, Confirmado, Token)";
                    query += " values(@nombre, @correo, @clave, @restablecer, @confirmado, @token)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@restablecer", usuario.Restablecer);
                    cmd.Parameters.AddWithValue("@confirmado", usuario.Confirmado);
                    cmd.Parameters.AddWithValue("@token", usuario.Token);
                    cmd.CommandType = System.Data.CommandType.Text;

                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Error en la base de datos: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en la base de datos: {ex.Message}");
            }


            return respuesta;
        }

        /// <summary>
        /// Valida las credenciales de un usuario comprobando su correo y clave.
        /// </summary>
        /// <param name="correo">El correo electrónico del usuario.</param>
        /// <param name="clave">La clave del usuario.</param>
        /// <returns>Un objeto `UsuarioDto` con los datos del usuario si las credenciales son válidas; de lo contrario, `null`.</returns>
        public UsuarioDto? Validar(string correo, string clave) {
            UsuarioDto? usuario = null;

            try {
                using (SqlConnection connection = new SqlConnection(_CadenaSQL)) {
                    string query = "select Nombre, Restablecer, Confirmado from usuarios";
                    query += " where Correo=@correo and Clave=@clave";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            usuario = new UsuarioDto(){
                                Nombre = reader["Nombre"].ToString(),
                                Restablecer = (bool)reader["Restablecer"],
                                Confirmado = (bool)reader["Confirmado"]
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Error en la base de datos: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en la base de datos: {ex.Message}");
            }

            return usuario;
        }

        /// <summary>
        /// Obtiene los datos completos de un usuario a partir de su correo electrónico.
        /// </summary>
        /// <param name="correo">El correo electrónico del usuario.</param>
        /// <returns>Un objeto `UsuarioDto` con los datos del usuario si se encuentra; de lo contrario, `null`.</returns>
        public UsuarioDto? Obtener(string correo)
        {
            UsuarioDto? usuario = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_CadenaSQL))
                {
                    string query = "select Nombre, Clave, Restablecer, Confirmado, Token from usuarios";
                    query += " where Correo=@correo";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            usuario = new UsuarioDto()
                            {
                                Nombre = reader["Nombre"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                Restablecer = (bool)reader["Restablecer"],
                                Confirmado = (bool)reader["Confirmado"],
                                Token = reader["Token"].ToString()
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Error en la base de datos: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en la base de datos: {ex.Message}");
            }


            return usuario;
        }

        /// <summary>
        /// Actualiza los campos `Restablecer` y `Clave` en la tabla `usuarios` de la base de datos, utilizando un `token` para identificar el usuario.
        /// </summary>
        /// <param name="restablecer">El estado de restablecimiento que se aplicará al usuario.</param>
        /// <param name="clave">La nueva clave para el usuario.</param>
        /// <param name="token">Token de usuario utilizado para identificar el registro a actualizar.</param>
        /// <returns>Devuelve `true` si la actualización se realizó con éxito; de lo contrario, `false`.</returns>
        public bool RestablecerActualizar(int restablecer, string clave, string token) {
            bool respuesta = false;

            try {
                using (SqlConnection connection = new SqlConnection(_CadenaSQL)) {
                    string query = @"update usuarios set Restablecer=@restablecer, Clave=@clave where Token=@token";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@restablecer",restablecer);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Error en la base de datos: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en la base de datos: {ex.Message}");
            }


            return respuesta;
        }

        /// <summary>
        /// Actualiza el campo `Confirmado` en la tabla `usuarios` de la base de datos a 1, utilizando el `token` para identificar el usuario.
        /// </summary>
        /// <param name="token">Token de usuario utilizado para identificar el registro a confirmar.</param>
        /// <returns>Devuelve `true` si la confirmación se realizó con éxito; de lo contrario, `false`.</returns>
        public bool Confirmar(string token) {
            bool respuesta = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(_CadenaSQL))
                {
                    string query = @"update usuarios set Confirmado=1 where Token=@token";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, $"Error en la base de datos: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en la base de datos: {ex.Message}");
            }


            return respuesta;
        }
    }
}
