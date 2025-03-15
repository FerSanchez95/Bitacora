using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Bitacora.Models
{
	//Modelo persistido
	public class ModeloBitacora
	{
		[Key]
		public int BitacoraId { get; set; }
		
		[Required]
		[StringLength(50, MinimumLength = 2)]
		public string NombreDeBitacora { get; set; }
		
		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateOnly FechaDeCreacion { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
		public TimeOnly HoraDeCreacion { get; set; }

		//Propiedad navegacional y relacional que vincula la bitacora con un usuario.
		public ModeloUsuario Propietario { get; set; }

		public int UsuarioId { get; set; }

		public List<ModeloPost> PostsRealizados { get; set; } = new List<ModeloPost>();


	}
}
