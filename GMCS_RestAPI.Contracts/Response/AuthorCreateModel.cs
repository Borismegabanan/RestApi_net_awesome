using System;

namespace GMCS_RestAPI.Contracts.Response
{
    public class AuthorCreateModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}