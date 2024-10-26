using System.Security.Cryptography;
using System.Text;

namespace StudentPortal.Services
{
    public static class UtilService
    {
        public static string ConvertirSHA256(string texto) {
            string hash = string.Empty;

            using (SHA256 sHA256 = SHA256.Create()) {
                byte[] hashValue = sHA256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                foreach (byte b in hashValue) {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }

        public static string GenerarToken() { 
            string token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}
