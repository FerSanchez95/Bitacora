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
            DateTime fechaHora = DateTime.Now;
            
            ModeloBitacora bitacora0 = new ModeloBitacora
            {
                NombreDeBitacora = "Proyecto_1",
                FechaDeCreacion = DateOnly.FromDateTime(fechaHora),
                HoraDeCreacion = TimeOnly.FromDateTime(fechaHora),
                UsuarioId = 1
            };

			ModeloBitacora bitacora1 = new ModeloBitacora
			{
				NombreDeBitacora = "Proyecto_43",
				FechaDeCreacion = DateOnly.FromDateTime(fechaHora),
				HoraDeCreacion = TimeOnly.FromDateTime(fechaHora),
				UsuarioId = 1
			};

			ModeloBitacora bitacora2 = new ModeloBitacora
			{
				NombreDeBitacora = "NuevoProyecto",
				FechaDeCreacion = DateOnly.FromDateTime(fechaHora),
				HoraDeCreacion = TimeOnly.FromDateTime(fechaHora),
				UsuarioId = 2
			};

			ModeloBitacora bitacora3 = new ModeloBitacora
			{
				NombreDeBitacora = "Notas-Personales",
				FechaDeCreacion = DateOnly.FromDateTime(fechaHora),
				HoraDeCreacion = TimeOnly.FromDateTime(fechaHora),
				UsuarioId = 2
			};

            bitacoras[0] = bitacora0;
			bitacoras[1] = bitacora1;
			bitacoras[2] = bitacora2;
			bitacoras[3] = bitacora3;

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
					FechaDeCreación = DateOnly.FromDateTime(fechaHoraPost.AddDays(-rand.Next(1, 30))), // Fecha aleatoria en los últimos 30 días
					HoraDeCreación = TimeOnly.FromDateTime(fechaHoraPost.AddHours(-rand.Next(1, 12))), // Hora aleatoria en las últimas 12 horas
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
