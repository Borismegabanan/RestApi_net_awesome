using System;
using System.ComponentModel.DataAnnotations;

namespace GMCS_RestApi.Domain.Commands
{
    public class CreateBookCommand
    {
        [Required(ErrorMessage = "название книги и автор обязательные поля")]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "название книги и автор обязательные поля")]
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
