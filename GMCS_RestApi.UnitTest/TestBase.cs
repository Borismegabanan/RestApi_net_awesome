using System.Threading;
using System.Threading.Tasks;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Contexts.Tools;

namespace GMCS_RestApi.UnitTests
{
    public class TestBase
    {
        private static readonly ApplicationContext _testContext = new TestDbContext();
        public static readonly ApplicationContext TestContext = LazyInitializer.EnsureInitialized(ref _testContext);

        public TestBase()
        {

        }

    }
}
