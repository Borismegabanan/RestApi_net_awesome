using System.ComponentModel.DataAnnotations;

namespace GMCS_RestApi.Domain.Models
{
	public class BookStateDbRecord
	{
		[Key] 
		public int Id { get; set; }
		public string Name { get; set; }
	}

}