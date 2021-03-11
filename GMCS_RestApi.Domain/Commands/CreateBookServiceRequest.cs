using System;

namespace GMCS_RestApi.Domain.Commands
{
    public class CreateBookServiceRequest
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
