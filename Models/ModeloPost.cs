using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bitacora.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bitacora.Models
{
	//Modelo persistido
	public class ModeloPost
	{
		[Key]
		public int PostID { get; set; }

		//TODO:  Agregar un mensaje de error.
		[Required]
		[StringLength(256, MinimumLength = 2)]
		public string Notas { get; set; }
		
		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
		public DateTime TimeStamp { get; set; }

		[DataType(DataType.DateTime)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
		public DateTime UltimaModificacion { get; set; }

		// Propiedades navegacionales y relacionales con ModeloBitacora.
		[ValidateNever]
		[ForeignKey("BitacoraId")]
		public ModeloBitacora BitacoraAsociada { get; set; }
		public int BitacoraId { get; set; }
	}
}
