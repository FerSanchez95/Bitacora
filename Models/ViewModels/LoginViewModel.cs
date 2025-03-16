using System.ComponentModel.DataAnnotations;
using Bitacora.Helpers;

namespace Bitacora.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = Mensajes.CampoRequerido)]
		public string NombreUsuario { get; set; }

		[Required(ErrorMessage = Mensajes.CampoRequerido), DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Recordarme")]
		public bool RememberMe { get; set; }
	}
}
