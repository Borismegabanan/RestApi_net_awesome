using System.ComponentModel.DataAnnotations;

namespace GMCS_RestAPI.Models
{
	public class BookStatus
	{
		[Key]
		public int Id { get; set; }
		
		//Rename to Title or Caption?
		public string Name { get; set; }
	}
}
