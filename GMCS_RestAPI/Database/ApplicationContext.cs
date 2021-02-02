using System;
using GMCS_RestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestAPI.Database
{
	public sealed class ApplicationContext : DbContext
	{
		public DbSet<Author> Authors { get; set; }

		public DbSet<Book> Books { get; set; }

		public DbSet<BookState> BookStates { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
