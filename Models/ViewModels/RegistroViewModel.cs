using System.ComponentModel.DataAnnotations;

namespace Bitacora.Models.ViewModels
{
	public class RegistroViewModel
	{
		[Required, StringLength(25, MinimumLength = 2)]
		[Display(Name = "Nombre de usuario")]
		public string NombreUsuario { get; set;}

		[Required, DataType(DataType.Password)]
		[Display(Name = "Contraseña")]
		public string Password { get; set; }

		[Required, Compare("Password")]
		[Display(Name = "Confirmar contraseña")]
		public string ConfirmedPassword { get; set; }
	}
}
