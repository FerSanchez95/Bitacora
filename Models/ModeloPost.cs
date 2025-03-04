using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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

		[DataType(DataType.Date)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateOnly FechaDeCreación { get; set; }

		[DataType(DataType.Time)]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
		public TimeOnly HoraDeCreación { get;set; }
	}
}
