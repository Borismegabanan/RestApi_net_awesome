using System;

namespace GMCS_RestApi.Domain.Commands
{
    public class CreateAuthorCommand
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
