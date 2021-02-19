using System;
using System.ComponentModel.DataAnnotations;

namespace GMCS_RestApi.Domain.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "название книги и автор обязательные поля")]
		public int AuthorId { get; set; }
		[Required(ErrorMessage = "название книги и автор обязательные поля")]
		public string Name { get; set; }
		public DateTime PublishDate { get; set; }
		[Range(1,3)]
		public int BookStateId { get; set; }
		public string WhoChanged { get; set; }
		public DateTime InitDate { get; set; }
	}
}
