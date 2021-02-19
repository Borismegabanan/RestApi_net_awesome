using System;

namespace GMCS_RestAPI.Contracts.Response
{
	public class BookResponse
	{
		public int Id { get; set; }

		public string Author { get; set; }

		public string Name { get; set; }

		public DateTime PublishDate { get; set; }

		public string BookState { get; set; }

		public string WhoChanged { get; set; }

		public DateTime InitDate { get; set; }
	}
}
