using GMCS_RestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestAPI.Contexts
{
	public class ApplicationContext : DbContext
	{
		public DbSet<BookStatus> BookStatuses { get; set; }

		public DbSet<Author> Authors { get; set; }

		public DbSet<Book> Books { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
