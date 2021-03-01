using System;
using System.ComponentModel.DataAnnotations;

namespace GMCS_RestAPI.Contracts.Request
{
    public class DeleteAuthorRequest
    {
        [Required(ErrorMessage = "Фамилия, имя и отчество обязательные поля")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Фамилия, имя и отчество обязательные поля")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Фамилия, имя и отчество обязательные поля")]
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}