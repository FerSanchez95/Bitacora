using Bitacora.Auth;
using Bitacora.Data;
using Bitacora.Models.ViewModels;
using Bitacora.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (ModelState.IsValid)
            {
                try
                {
					await _administrarUsuario.NuevoUsuario(registro);
                    return RedirectToAction(nameof(Index), "Home");
				}
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "No se pudo registrar un nuevo usuario");
                }
            }

            return View(registro);
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        public async Task<IActionResult> IniciarSesion(LoginViewModel logIn)
        {
            if (ModelState.IsValid)
            {
                // Lógica de acceso.
            }
            return View(logIn);
        }

        public async Task<IActionResult> CerrarSesion()
        {
            // Lógica de cierre.
            return View();
        }
    }
}
