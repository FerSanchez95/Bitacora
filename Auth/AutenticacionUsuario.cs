using Microsoft.AspNetCore.Identity;
using Bitacora.Models;

namespace Bitacora.Auth
{
	public class AutenticacionUsuario : IdentityUser<int>
	{
		public ModeloUsuario Usuario { get; set; }
	}
}
