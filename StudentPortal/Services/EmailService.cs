using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class EmailService
    {
        private readonly string _host;
        private readonly int _puerto;

        private readonly string _nombreEnvia;
        private readonly string _correo;
        private readonly string _clave;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _host = configuration["EmailSettings:Host"];
            _puerto = int.Parse(configuration["EmailSettings:Puerto"]);
            _nombreEnvia = configuration["EmailSettings:NombreEnvia"];

            //Secret Settings
            _correo = configuration["EmailSecretSettings:Correo"];
            _clave = configuration["EmailSecretSettings:Clave"];
            _logger = logger;
        }


        /// <summary>
        /// Envía un correo electrónico utilizando los detalles proporcionados en `CorreoDto`.
        /// </summary>
        /// <param name="correoDto">DTO que contiene los detalles del correo como destinatario, asunto y contenido.</param>
        /// <returns>Devuelve `true` si el correo se envía correctamente, de lo contrario `false`.</returns>
        public bool Enviar(CorreoDto correoDto) {

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_nombreEnvia, _correo));
            email.To.Add(MailboxAddress.Parse(correoDto.Para));
            email.Subject = correoDto.Asunto;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = correoDto.Contenido,
            };

            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_host, _puerto, SecureSocketOptions.StartTls);
                smtp.Authenticate(_correo, _clave);
                smtp.Send(email);
                smtp.Disconnect(true);

                _logger.LogInformation("Correo enviado exitosamente a {Para}", correoDto.Para);
                return true;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error al enviar el correo a {Para}", correoDto.Para);
                return false;
            }
        }
    }
}
