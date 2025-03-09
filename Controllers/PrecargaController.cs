using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bitacora.Models;
using Bitacora.Auth;
using Bitacora.Data;
using Bitacora.Services;
using Microsoft.EntityFrameworkCore;
using Bitacora.Models.ViewModels;

namespace Bitacora.Controllers
{
    public class PrecargaController : Controller
    {
        private readonly UserManager<AutenticacionUsuario> _userManager;
        private readonly SignInManager<AutenticacionUsuario> _signInManager;
        private readonly BitacoraDb _dbContext;
        private readonly AdministrarUsuario _administrarUsuario;

        public PrecargaController(UserManager<AutenticacionUsuario> userManager,
                                    SignInManager<AutenticacionUsuario> signInManager,
                                    BitacoraDb bitacoraDb,
                                    AdministrarUsuario administrarUsuario)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._dbContext = bitacoraDb;
            this._administrarUsuario = administrarUsuario;
        }

        public IActionResult Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.Migrate();

             this.CrearUsuarios().Wait();


            return RedirectToAction("Index", "Home");
        }
        private async Task CrearUsuarios()
        {
            // Acá se va a invocar un servisio de creación de usuarios.
            string[] nombresDeUsuarios = { "Usuario1", "Usuario2" };

			await _administrarUsuario.PrecargaDeUsuarios(nombresDeUsuarios);
        }
    }
}
