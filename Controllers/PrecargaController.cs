using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bitacora.Models;
using Bitacora.Auth;
using Bitacora.Data;
using Microsoft.EntityFrameworkCore;
using Bitacora.Models.ViewModels;

namespace Bitacora.Controllers
{
    public class PrecargaController : Controller
    {
        private readonly UserManager<AutenticacionUsuario> _userManager;
        private readonly SignInManager<AutenticacionUsuario> _signInManager;
		private readonly BitacoraDb _dbContext;

        public PrecargaController(UserManager<AutenticacionUsuario> userManager,
                                    SignInManager<AutenticacionUsuario> signInManager,
                                    BitacoraDb bitacoraDb)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._dbContext = bitacoraDb;
        }

        public IActionResult Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.Migrate();


            return RedirectToAction("Index", "Home");
        }
        private async Task CrearUsuarios()
        {
            // Acá se va a invocar un servisio de creación de usuarios.

        }
    }
}
