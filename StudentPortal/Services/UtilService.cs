using System.Security.Cryptography;
using System.Text;

namespace StudentPortal.Services
{
    public class UtilService
    {
        /// <summary>
        /// Convierte un texto en un hash SHA-256.
        /// </summary>
        /// <param name="texto">El texto que se va a convertir a hash.</param>
        /// <returns>Una cadena hexadecimal que representa el hash SHA-256 del texto.</returns>
        public string ConvertirSHA256(string texto) {
            string hash = string.Empty;

            using (SHA256 sHA256 = SHA256.Create()) {
                byte[] hashValue = sHA256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                foreach (byte b in hashValue) {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }

        /// <summary>
        /// Convierte un texto en un hash SHA-256 de forma asíncrona.
        /// </summary>
        /// <param name="texto">El texto que se va a convertir a hash.</param>
        /// <returns>Una cadena hexadecimal que representa el hash SHA-256 del texto.</returns>
        public async Task<string> ConvertirSHA256Async(string texto)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(texto);

                // Calcular el hash de forma asíncrona
                byte[] hashValue = await Task.Run(() => sha256.ComputeHash(textBytes));

                // Convertir el hash a una cadena hexadecimal
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }

        /// <summary>
        ///     Genera un token único usando un GUID.
        /// </summary>
        /// <returns>Una cadena única sin guiones que representa el token.</returns>
        public string GenerarToken() { 
            string token = Guid.NewGuid().ToString("N");
            return token;
        }


        /// <summary>
        /// Ejecuta dos tareas asíncronas en paralelo para generar números aleatorios y muestra los resultados.
        /// </summary>
        /// <remarks>
        /// Utiliza <see cref="Task.Run"/> para iniciar las tareas en paralelo y <see cref="Random.Shared"/> 
        /// para generar números de manera segura entre hilos.
        /// </remarks>
        /// <returns>Una <see cref="Task"/> que representa la operación asíncrona.</returns>
        public static async Task RandomNumberAsync()
        {
            var task1 = Task.Run(() => Random.Shared.Next(1000));
            var task2 = Task.Run(() => Random.Shared.Next(1000));

            Console.WriteLine("Start Async Methods");

            var result1 = await task1;
            var result2 = await task2;

            Console.WriteLine("End Async Methods");
            Console.WriteLine($"Values are: {result1} and {result2}");
        }
    }
}
