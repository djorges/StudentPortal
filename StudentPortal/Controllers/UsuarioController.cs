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
                else if (usuario.Restablecer)
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
        public async Task<ViewResult> Registrar(UsuarioDto usuario) {
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


                    CorreoDto correoDto = new CorreoDto()
                    {
                        Para = usuario.Correo,
                        Asunto = "Correo de Confirmación",
                        Contenido = string.Format(contenido, usuario.Nombre, baseUrl)
                    };
                    bool enviado = _emailService.Enviar(correoDto);

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

        [HttpGet]
        public ViewResult Confirmar(string token) {
            ViewBag.Respuesta = _dbUsuario.Confirmar(token);
            return View();
        }

        [HttpGet]
        public ViewResult Restablecer() { 
            return View(); 
        }

        [HttpPost]
        public async Task<ViewResult> Restablecer(string correo) {
            UsuarioDto? usuario = _dbUsuario.Obtener(correo);
            ViewBag.Correo = correo;
            if (usuario != null) {
                bool respuesta = _dbUsuario.RestablecerActualizar(1, usuario.Clave, usuario.Token);

                if (respuesta)
                {
                    string rutaArchivoHtml = Path.Combine(_env.ContentRootPath, "Template", "Restablecer.html");
                    string contenido = await System.IO.File.ReadAllTextAsync(rutaArchivoHtml);

                    //Generar url
                    string baseUrl = $"{Request.Scheme}://{Request.Host.Host}:{Request.Host.Port}/Usuario/Actualizar?token={usuario.Token}";

                    CorreoDto correoDto = new CorreoDto()
                    {
                        Para = correo,
                        Asunto = "Restablecer Contraseña",
                        Contenido = string.Format(contenido, usuario.Nombre, baseUrl)
                    };
                    bool enviado = _emailService.Enviar(correoDto);

                    ViewBag.Restablecido = true;
                    ViewBag.Mensaje = "Su contraseña fue restablecida, le enviamos un mensaje a su correo para restablecer.";
                }
                else {
                    ViewBag.Mensaje = "No se pudo restablecer la cuenta";
                }
            }
            else {
                ViewBag.Mensaje = "No se encontraron coincidencias con el correo";
            }
            return View(); 
        }

        [HttpGet]
        public ViewResult Actualizar(string token) {
            ViewBag.Token = token;

            return View();
        }

        [HttpPost]
        public ViewResult Actualizar(string token, string clave, string confirmarClave) {
            ViewBag.Token = token;
            if (clave != confirmarClave) {
                ViewBag.Mensaje = "Las contraseñas no coinciden";

                return View();
            }
            
            bool respuesta = _dbUsuario.RestablecerActualizar(0, _utilService.ConvertirSHA256(clave), token);
            if (respuesta)
            {
                ViewBag.Restablecido = true;
            }
            else {
                ViewBag.Mensaje = "No se pudo actualizar";
            }

            return View();
        }
    }
}
