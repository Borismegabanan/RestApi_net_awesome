using System;
using System.ComponentModel.DataAnnotations;

namespace GMCS_RestApi.Domain.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
