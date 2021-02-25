using System;

namespace GMCS_RestAPI.Contracts.Response
{
	public class AuthorModel
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
