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
				bitacoras[i] = new ModeloBitacora
				{
					NombreDeBitacora = $"Bitacora_{i + 1}",
					FechaDeCreacion = DateOnly.FromDateTime(fechaHoraBitacora.AddDays(-rand.Next(1, 30))), // Fecha aleatoria en los últimos 30 días
					HoraDeCreacion = TimeOnly.FromDateTime(fechaHoraBitacora.AddHours(-rand.Next(1, 12))), // Hora aleatoria en las últimas 12 horas
					UsuarioId = rand.Next(1, 3) // Bitácoras del 1 al 4
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
				posts[i] = new ModeloPost
				{
					Notas = $"Nota de prueba {i + 1} - Lorem ipsum dolor sit amet.",
					FechaDeCreacion = DateOnly.FromDateTime(fechaHoraPost.AddDays(-rand.Next(1, 30))), // Fecha aleatoria en los últimos 30 días
					HoraDeCreacion = TimeOnly.FromDateTime(fechaHoraPost.AddHours(-rand.Next(1, 12))), // Hora aleatoria en las últimas 12 horas
					BitacoraId = rand.Next(1, 5) // Bitácoras del 1 al 4
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
