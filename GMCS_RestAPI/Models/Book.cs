using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMCS_RestAPI.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "название книги и автор обязательные поля")]
		public Author Author { get; set; }

		[Required(ErrorMessage = "название книги и автор обязательные поля")]
		public string Name { get; set; }

		public DateTime PublishDate { get; set; }

		public BookStatus BookStatus { get; set; }


		public string WhoChanged { get; set; }

		public DateTime InitDate { get; set; }
	}
}
