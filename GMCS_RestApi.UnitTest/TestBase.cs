using GMCS_RestApi.Domain.Contexts;
using System;

namespace GMCS_RestApi.UnitTests
{
    public class TestBase
    {
        private static readonly Lazy<ApplicationContext> _testContext =
            new Lazy<ApplicationContext>(new TestDbContext());

        public static readonly ApplicationContext TestContext = _testContext.Value;

        public TestBase()
        {

        }

    }
}
