using System.ComponentModel.DataAnnotations;
using Bitacora.Helpers;

namespace Bitacora.Models.ViewModels
{
	public class RegistroViewModel
	{
		[Required, StringLength(25, MinimumLength = 2, ErrorMessage = Mensajes.CantidadDeCaracteres)]
		[Display(Name = "Nombre de usuario")]
		public string NombreUsuario { get; set;}

		[Required, DataType(DataType.Password)]
		[Display(Name = "Contraseña")]
		[StringLength(30, MinimumLength = 8, ErrorMessage = Mensajes.LongitudPassword)]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
			ErrorMessage = Mensajes.CaracteresPassword)]
		public string Password { get; set; }

		[Required(ErrorMessage = Mensajes.confirmacionRequerida), Compare("Password")]
		[Display(Name = "Confirmar contraseña")]
		public string ConfirmedPassword { get; set; }
	}
}
