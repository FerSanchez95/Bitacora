using System.ComponentModel.DataAnnotations;

namespace Bitacora.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		public string NombreUsuario { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
