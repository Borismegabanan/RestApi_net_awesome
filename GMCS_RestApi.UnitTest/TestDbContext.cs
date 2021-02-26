using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Contexts.Tools;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestApi.UnitTests
{
	public class TestDbContext: ApplicationContext
	{
		public TestDbContext()
		{
		}

		public TestDbContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseInMemoryDatabase("TestDb");
			}
		}
	}
}
