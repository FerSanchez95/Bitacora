using System;
using System.Threading.Tasks;
using Bitacora.Auth;
using Bitacora.Data;
using Bitacora.Models;
using Bitacora.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bitacora.Services
{
	public class AdministrarUsuario
	{
		private readonly BitacoraDb _dbContext;
		private readonly UserManager<AutenticacionUsuario> _userManager;
		private readonly SignInManager<AutenticacionUsuario> _signInManager;

		public AdministrarUsuario(UserManager<AutenticacionUsuario> userManager,
							SignInManager<AutenticacionUsuario> signInManager,
							BitacoraDb dbContext)
		{
			this._dbContext = dbContext;
			this._userManager = userManager;
			this._signInManager = signInManager;
		}

		public async Task PrecargaDeUsuarios(string[] nombresUsuarios)
		{
			foreach (var nombre in nombresUsuarios)
			{
				RegistroViewModel nuevoRegistro = new RegistroViewModel()
				{
					NombreUsuario = nombre,
					Password = "Password1!"
				};

				await this.NuevoUsuario(nuevoRegistro);

			}
		}

		public async Task NuevoUsuario(RegistroViewModel registro)
		{
			// La validación del registro debe hacerse en una función de orden superior.
			AutenticacionUsuario nuevoRegistro = new AutenticacionUsuario();
			nuevoRegistro.UserName = registro.NombreUsuario;

			var resultadoAutenticacion = await _userManager.CreateAsync(nuevoRegistro, registro.Password);

			if (resultadoAutenticacion.Succeeded)
			{
				ModeloUsuario nuevoUsuario = new ModeloUsuario()
				{
					NombreUsuario = registro.NombreUsuario,
					CantidadDeBitacoras = 0,
					AutenticacionId = nuevoRegistro.Id
				};

				 await this.GuardarUsuario(nuevoUsuario);
			}
		}

		private async Task GuardarUsuario(ModeloUsuario usuario)
		{
			try
			{
				_dbContext.Usuarios.Add(usuario);
				await _dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}
		}

	}
}
