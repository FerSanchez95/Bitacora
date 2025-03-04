using System.ComponentModel.DataAnnotations;
using Bitacora.Auth;

namespace Bitacora.Models
{
	//Modelo persistido
	public class ModeloUsuario
	{
		[Key]
		public int UsuarioId { get; set; }

		[Required]
		public string NombreUsuario { get; set; }

		public int CantidadDeBitacoras { get; set; }

		// Propiedad navegacional y relacional de autenticación.
		public AutenticacionUsuario PerfilDeAutenticacion { get; set; }
		public int AutenticacionId { get; set; }

	}
}
