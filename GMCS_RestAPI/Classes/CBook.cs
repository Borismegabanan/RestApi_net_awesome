﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GMCS_RestAPI.Classes
{
	public class CBook
	{
		public int Id { get; set; }

		public string Author { get; set; }

		public string Name { get; set; }

		public DateTime PublishDate { get; set; }

		public string BookStatus { get; set; }

		public string WhoChanged { get; set; }

		public DateTime InitDate { get; set; }
	}
}