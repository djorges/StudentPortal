using Microsoft.AspNetCore.Mvc;
using MimeKit;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Services;


namespace StudentPortal.Controllers
{

    public class UsuarioController: Controller
    {
        private readonly DBUsuario _dbUsuario;
        private readonly UtilService _utilService;
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _env;

        public UsuarioController(DBUsuario dbUsuario, UtilService utilService, EmailService emailService, IWebHostEnvironment env)
        {
            _dbUsuario = dbUsuario;
            _utilService = utilService;
            _emailService = emailService;
            _env = env;
        }

        [HttpGet]
        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string clave) {
            UsuarioDto? usuario = _dbUsuario.Validar(correo, _utilService.ConvertirSHA256(clave));

            if (usuario != null)
            {
                if (!usuario.Confirmado)
                {
                    ViewBag.Mensaje = $"Falta confirmar su cuenta. Se le envio un correo a {correo}";
                }
                else if (!usuario.Restablecer)
                {
                    ViewBag.Mensaje = $"Se ha solicitado restablecer su cuenta, por favor revise su bandeja del correo {correo}";
                }
                else {
                    return RedirectToAction("Index", "Home");
                }
            }
            else {
                ViewBag.Mensaje = "No se encontraron coincidencias";
            }

            return View();
        }

        [HttpGet]
        public ViewResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(UsuarioDto usuario) {
            if (usuario.Clave != usuario.ConfirmarClave) {
                //TODO: Use Validation
                ViewBag.Nombre = usuario.Nombre;
                ViewBag.Correo = usuario.Correo;
                ViewBag.Mensaje = "Las contraseñas no coinciden";

                return View();
            }

            if (_dbUsuario.Obtener(usuario.Correo) == null) {
                usuario.Clave = _utilService.ConvertirSHA256(usuario.Clave);
                usuario.Token = _utilService.GenerarToken();
                usuario.Restablecer = false;
                usuario.Confirmado = false;

                //Confirmar cuenta
                if (_dbUsuario.Registrar(usuario)){
                    string rutaArchivoHtml = Path.Combine(_env.ContentRootPath, "Template", "Confirmar.html");
                    string contenido = await System.IO.File.ReadAllTextAsync(rutaArchivoHtml);
                    
                    //Generar url
                    string baseUrl = $"{Request.Scheme}://{Request.Host.Host}:{Request.Host.Port}/Usuario/Confirmar?token={usuario.Token}";

                    _emailService.Enviar(
                        new CorreoDto()
                        {
                            Para = usuario.Correo,
                            Asunto = "Correo de Confirmación",
                            Contenido = string.Format(contenido, usuario.Nombre, baseUrl)
                        }
                    );

                    ViewBag.Creado = true;
                    ViewBag.Mensaje = $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {usuario.Correo} para confirmar su cuenta";
                }else {
                    ViewBag.Mensaje = "No se pudo crear su cuenta";
                }
            }else{
                ViewBag.Mensaje = "El correo ya se encuentra registrado";
            }

            return View();
        }
    }
}
