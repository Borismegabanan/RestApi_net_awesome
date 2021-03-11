using System.IO;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GMCS_RestApi.Domain.Contexts
{
    public class ApplicationContext : DbContext
    {
        private readonly StreamWriter _logStream = new StreamWriter("log", true);

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookStateDbRecord> BookStates { get; set; }

        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(_logStream.WriteLine);

        }
        public override void Dispose()
        {
            base.Dispose();
            _logStream.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await _logStream.DisposeAsync();
        }
    }
}
