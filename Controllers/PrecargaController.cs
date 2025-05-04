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
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;

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
            this.CrearBitacoras().Wait();
            this.CrearPost().Wait();


            return RedirectToAction("Index", "Home");
        }
        private async Task CrearUsuarios()
        {
            // Acá se va a invocar un servisio de creación de usuarios.
            string[] nombresDeUsuarios = { "Usuario1", "Usuario2" };

			await _administrarUsuario.PrecargaDeUsuarios(nombresDeUsuarios);
        }

        private async Task CrearBitacoras()
        {
            ModeloBitacora[] bitacoras = new ModeloBitacora[4];
            DateTime fechaHoraBitacora = DateTime.Now;
			Random rand = new Random();

			for (int i = 0; i < bitacoras.Length; i++)
			{
				DateTime fechaACargar = fechaHoraBitacora.AddDays(-rand.Next(1, 30))
						.AddHours(-rand.Next(0, 12))
						.AddMinutes(-rand.Next(0, 60))
						.AddSeconds(-rand.Next(0, 60));

				bitacoras[i] = new ModeloBitacora
				{
					NombreDeBitacora = $"Bitacora_{i + 1}",
					TimeStamp = fechaACargar,
					UltimaModificacion = fechaACargar,
					UsuarioId = rand.Next(1, 3) 
				};
			}

            foreach (var bitacora in bitacoras)
            {
                _dbContext.Add(bitacora);
            }

			await _dbContext.SaveChangesAsync();
		}

        private async Task CrearPost()
        {
            ModeloPost[] posts = new ModeloPost[20];
            DateTime fechaHoraPost = DateTime.Now;
			Random rand = new Random();

			for (int i = 0; i < posts.Length; i++)
			{
                DateTime fechaACargar = fechaHoraPost.AddDays(-rand.Next(1, 30))
                        .AddHours(-rand.Next(0, 12))
                        .AddMinutes(-rand.Next(0, 60))
                        .AddSeconds(-rand.Next(0, 60));

				posts[i] = new ModeloPost
                {
                    Notas = $"Nota de prueba {i + 1} - Lorem ipsum dolor sit amet.",
                    TimeStamp = fechaACargar,
                    UltimaModificacion = fechaACargar,
                    BitacoraId = rand.Next(1, 5)
                };
			}

			foreach (var p in posts)
            {
                _dbContext.Add(p);
            }

			await _dbContext.SaveChangesAsync();
		}
    }
}
