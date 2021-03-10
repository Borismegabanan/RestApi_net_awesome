using System;

namespace GMCS_RestAPI.Contracts.Request
{

    public class CreateAuthorRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}