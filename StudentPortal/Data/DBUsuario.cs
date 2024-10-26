using StudentPortal.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace StudentPortal.Data{

    public class DBUsuario{

        private static string CadenaSQL = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_studentportal;Integrated Security=True;Trust Server Certificate=False;Encrypt=False;";

        public static bool Registrar(UsuarioDto usuario) { 
            bool respuesta = false;

            try{
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    string query = "insert into usuarios(Nombre, Correo, Clave, Restablecer, Confirmado, Token)";
                    query += "values(@nombre, @correo, @clave, @restablecer, @confirmado, @token)";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("@restablecer", usuario.Restablecer);
                    cmd.Parameters.AddWithValue("@confirmado", usuario.Confirmado);
                    cmd.CommandType = System.Data.CommandType.Text;

                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }
            }catch (Exception e) { 
                Console.WriteLine(e.ToString());
            }

            return respuesta;
        }

        public static UsuarioDto? Validar(string correo, string clave) {
            UsuarioDto? usuario = null;

            try {
                using (SqlConnection connection = new SqlConnection(CadenaSQL)) {
                    string query = "select Nombre, Restablecer, Confirmado from usuarios";
                    query += "where Correo = @correo and Clave = @clave";

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
                Console.WriteLine($"Error en la base de datos: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }

            return usuario;
        }

        public static UsuarioDto? Obtener(string correo)
        {
            UsuarioDto? usuario = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    string query = "select Nombre, Clave, Restablecer, Confirmado, Token from usuarios";
                    query += "where Correo= @correo";

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
                Console.WriteLine($"Error en la base de datos: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }

            return usuario;
        }

        public static bool RestablecerActualizar(int restablecer, string clave, string token) {
            bool respuesta = false;

            try {
                using (SqlConnection connection = new SqlConnection(CadenaSQL)) {
                    string query = @"update usuarios set Restablecer = @restablecer, Clave = @clave where Token = @token";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@restablecer",restablecer);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }
            }catch (SqlException sqlEx){
                Console.WriteLine($"Error en la base de datos: {sqlEx.Message}");
            }catch (Exception ex){
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }

            return respuesta;
        }

        public static bool Confirmar(string token) {
            bool respuesta = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(CadenaSQL))
                {
                    string query = @"update usuarios set Confirmado=1 where Token=@token";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@token", token);
                    cmd.CommandType = CommandType.Text;

                    connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) respuesta = true;
                }
            }catch (SqlException sqlEx){
                Console.WriteLine($"Error en la base de datos: {sqlEx.Message}");
            }catch (Exception ex){
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }

            return respuesta;
        }
    }
}
