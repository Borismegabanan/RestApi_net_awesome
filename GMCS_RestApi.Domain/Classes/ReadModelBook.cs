using System;

namespace GMCS_RestApi.Domain.Classes
{
	public class ReadModelBook
	{
		public int Id { get; set; }

		public string Author { get; set; }

		public string Name { get; set; }

		public DateTime PublishDate { get; set; }

		public int BookStateId { get; set; }

		public string WhoChanged { get; set; }

		public DateTime InitDate { get; set; }
	}
}