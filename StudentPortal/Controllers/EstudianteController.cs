using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using StudentPortal.Data;
using StudentPortal.Entities;
using StudentPortal.Models;
using StudentPortal.Services;


namespace StudentPortal.Controllers
{

    public class EstudianteController: Controller
    {
        private readonly EstudianteService _estudianteService;
        private readonly UtilService _utilService;
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _env;
        const string SessionUserId = "_UserId";
        const string SessionUserName = "_UserName";
        const string SessionUserToken = "_UserToken";

        public EstudianteController(EstudianteService estudianteService, UtilService utilService, EmailService emailService, IWebHostEnvironment env)
        {
            _estudianteService = estudianteService;
            _utilService = utilService;
            _emailService = emailService;
            _env = env;
        }

        [HttpGet]
        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string correo, string clave) {
            EstudianteDto? usuario = await _estudianteService.ValidarAsync(correo, _utilService.ConvertirSHA256(clave));

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
                    if (usuario.Token != null) {
                        //Crear sesion para el usuario logueado
                        HttpContext.Session.SetInt32(SessionUserId, usuario.EstudianteId);
                        HttpContext.Session.SetString(SessionUserToken, usuario.Token.ToString());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            else {
                ViewBag.Mensaje = "No se encontraron coincidencias";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ViewResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Registrar(EstudianteDto usuario) {
            if (usuario.Clave != usuario.ConfirmarClave) {
                //TODO: Use Validation
                ViewBag.Nombre = usuario.Nombre;
                ViewBag.Correo = usuario.Correo;
                ViewBag.Mensaje = "Las contraseñas no coinciden";

                return View();
            }

            var estudiante = await _estudianteService.ObtenerPorCorreoAsync(usuario.Correo);
            if (estudiante == null) {
                usuario.Clave = _utilService.ConvertirSHA256(usuario.Clave);
                usuario.Token = _utilService.GenerarToken();
                usuario.Restablecer = false;
                usuario.Confirmado = false;

                //Confirmar cuenta
                if (await _estudianteService.RegistrarAsync(usuario)){
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
        public async Task<ViewResult> Confirmar(string token) {
            ViewBag.Respuesta = await _estudianteService.ConfirmarAsync(token);
            return View();
        }

        [HttpGet]
        public ViewResult Restablecer() { 
            return View(); 
        }

        [HttpPost]
        public async Task<ViewResult> Restablecer(string correo) {
            EstudianteDto? usuario = await _estudianteService.ObtenerPorCorreoAsync(correo);
            ViewBag.Correo = correo;
            if (usuario != null) {
                bool respuesta = await _estudianteService.RestablecerClaveAsync(1, usuario.Clave, usuario.Token);

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
        public async Task<ViewResult> Actualizar(string token, string clave, string confirmarClave) {
            ViewBag.Token = token;

            if (clave != confirmarClave) {
                ViewBag.Mensaje = "Las contraseñas no coinciden";

                return View();
            }
            
            bool respuesta = await _estudianteService.RestablecerClaveAsync(0, _utilService.ConvertirSHA256(clave), token);
            if (respuesta)
            {
                ViewBag.Restablecido = true;
            }
            else {
                ViewBag.Mensaje = "No se pudo actualizar";
            }

            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Perfil(int id) { 
            var estudiante = await _estudianteService.ObtenerPorIdAsync(id);
            int? userId = HttpContext?.Session.GetInt32("_UserId");
            
            if (estudiante == null || userId == null ) {
                return RedirectToAction("Index","Home");
            }
            
            ViewBag.Id = userId;
            return View(estudiante);
        }

        [HttpPost]
        public async Task<ActionResult> Perfil(int id, EstudianteDto estudiante)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _estudianteService.ActualizarAsync(id, estudiante);
                if (resultado) {
                    ViewBag.Mensaje = "Se actualizó los datos correctamente";
                }
                else {
                    ViewBag.Mensaje = "Ocurrió un error al actualizar los datos.";
                }
            }
            else {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(estudiante);
        }
    }
}
