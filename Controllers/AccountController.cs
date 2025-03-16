using Bitacora.Auth;
using Bitacora.Data;
using Bitacora.Models.ViewModels;
using Bitacora.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bitacora.Controllers
{
    public class AccountController : Controller
    {
        private readonly BitacoraDb _bitacoraDb;
        private readonly SignInManager<AutenticacionUsuario> _signInManager;
        private readonly UserManager<AutenticacionUsuario> _userManager;
        private readonly AdministrarUsuario _administrarUsuario;
        public AccountController(BitacoraDb bitacoraDb, 
            SignInManager<AutenticacionUsuario> signInManager, 
            UserManager<AutenticacionUsuario> userManager,
            AdministrarUsuario administrarUsuario)
        {
            _bitacoraDb = bitacoraDb;
            _signInManager = signInManager;
            _userManager = userManager;
            _administrarUsuario = administrarUsuario;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroViewModel registro)
        {
            if (!ModelState.IsValid)
            {
                return View(registro);
            }

            var resultadoRegistro = await _administrarUsuario.RegistrarUsuario(registro);

            if (!resultadoRegistro.FueExitoso)
            {
                ModelState.AddModelError(string.Empty, resultadoRegistro.Mensaje);
                return View(registro);
            }

            ViewData["MensajeRegistro"] = resultadoRegistro.Mensaje;
            return View();
            // Acá debe redirigir a una página de resultado exitoso.
            // La misma debe tener un botón de redirección.
        }

        public IActionResult IniciarSesion(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(LoginViewModel logIn)
        {
            string? returnUrl = TempData["ReturnUrl"] as string;

            if (!ModelState.IsValid)
            {
				return View(logIn);
			}

			var resultadoInicioSesion = await _administrarUsuario.IniciarSesionUsuario(logIn);

			if (!resultadoInicioSesion.FueExitoso)
			{
				ModelState.AddModelError(string.Empty, resultadoInicioSesion.Mensaje);
				return View(logIn);
			}
            
            TempData["InicioExitoso"] = resultadoInicioSesion.Mensaje;

            if (!returnUrl.IsNullOrEmpty())
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");	
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await _administrarUsuario.CerrarSesionUsuario();
            return RedirectToAction("IniciarSesion");
        }
    }
}
