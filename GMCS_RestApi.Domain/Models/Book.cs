using System;
using System.ComponentModel.DataAnnotations;

namespace GMCS_RestApi.Domain.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public int BookStateId { get; set; }
        public string WhoChanged { get; set; }
        public DateTime InitDate { get; set; }
    }
}
