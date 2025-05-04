using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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

		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
		public DateTime TimeStamp { get; set; }

		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
		public DateTime UltimaModificacion { get; set; }

		//Propiedad navegacional y relacional que vincula la bitacora con un usuario.
		[ForeignKey("UsuarioId")]
		[ValidateNever]
		public ModeloUsuario Propietario { get; set; }

		public int UsuarioId { get; set; }

		public List<ModeloPost> PostsRealizados { get; set; } = new List<ModeloPost>();


	}
}
