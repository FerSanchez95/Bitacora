using System;
using System.Threading.Tasks;
using Bitacora.Auth;
using Bitacora.Data;
using Bitacora.Models;
using Bitacora.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bitacora.Helpers;
using NuGet.Packaging;

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

				await this.RegistrarUsuario(nuevoRegistro);

			}
		}

		public async Task<(bool FueExitoso, string Mensaje)> RegistrarUsuario(RegistroViewModel registro)
		{
			// La validación del registro debe hacerse en una función de orden superior.
			AutenticacionUsuario nuevoRegistro = new AutenticacionUsuario();
			nuevoRegistro.UserName = registro.NombreUsuario;

			var validarPassword = await _userManager.PasswordValidators.First().ValidateAsync(_userManager, null, registro.Password);

			if (!validarPassword.Succeeded)
			{
				return (false, Mensajes.PasswordNoValido);
			}

			var resultadoAutenticacion = await _userManager.CreateAsync(nuevoRegistro, registro.Password);

			if (!resultadoAutenticacion.Succeeded)
			{
				return (false, Mensajes.RegistroInvalido);
			}

			ModeloUsuario nuevoUsuario = new ModeloUsuario()
			{
				NombreUsuario = registro.NombreUsuario,
				CantidadDeBitacoras = 0,
				AutenticacionId = nuevoRegistro.Id
			};

			try
			{
				await this.GuardarUsuario(nuevoUsuario);
			}
			catch (DbUpdateConcurrencyException)
			{
				return (false, Mensajes.ErrorCargaBd);
			}

			return (true, Mensajes.RegistroExitoso);
		}

		public async Task<(bool FueExitoso, string Mensaje)> IniciarSesionUsuario(LoginViewModel logIn)
		{
			var resultadoAutenticacion = await _signInManager.PasswordSignInAsync(logIn.NombreUsuario,
																					logIn.Password,
																					logIn.RememberMe,
																					false);
			if (!resultadoAutenticacion.Succeeded)
			{
				return (false, Mensajes.InicioInvalido);
			}

			return (true, Mensajes.InicioExitoso);
		}

		public async Task CerrarSesionUsuario()
		{
			await _signInManager.SignOutAsync();
		}

		private async Task GuardarUsuario(ModeloUsuario usuario)
		{
			_dbContext.Usuarios.Add(usuario);
			await _dbContext.SaveChangesAsync();
		}
	}
}
