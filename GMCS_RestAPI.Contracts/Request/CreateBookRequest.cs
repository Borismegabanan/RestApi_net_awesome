using System;

namespace GMCS_RestAPI.Contracts.Request
{

    public class CreateBookRequest
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public DateTime PublishDate { get; set; } }
}