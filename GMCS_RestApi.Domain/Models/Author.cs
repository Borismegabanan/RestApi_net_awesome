using System;
using System.ComponentModel.DataAnnotations;

namespace GMCS_RestApi.Domain.Models
{
	public class Author
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Фамилия, имя и отчество обязательные поля")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Фамилия, имя и отчество обязательные поля")]
		public string Surname { get; set; }

		[Required(ErrorMessage = "Фамилия, имя и отчество обязательные поля")]
		public string MiddleName { get; set; }

		public string FullName { get; set; }

		public DateTime BirthDate { get; set; }
	}
}
