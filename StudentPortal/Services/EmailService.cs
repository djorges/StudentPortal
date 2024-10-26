using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class EmailService
    {
        private readonly string _Host;
        private readonly int _Puerto;

        private readonly string _NombreEnvia;
        private readonly string _Correo;
        private readonly string _Clave;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _Host = configuration["EmailSettings:Host"];
            _Puerto = int.Parse(configuration["EmailSettings:Puerto"]);

            _NombreEnvia = configuration["EmailSettings:NombreEnvia"];
            _Correo = configuration["EmailSettings:Correo"];
            _Clave = configuration["EmailSettings:Clave"];
            _logger = logger;
        }

        public bool Enviar(CorreoDto correoDto) {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_NombreEnvia, _Correo));
                email.To.Add(MailboxAddress.Parse(correoDto.Para));
                email.Subject = correoDto.Asunto;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = correoDto.Contenido,
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host,_Puerto, SecureSocketOptions.StartTls);
                smtp.Authenticate(_Correo,_Clave);
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
