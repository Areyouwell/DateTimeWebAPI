using DTWebAPI.Services;

namespace DateTimeTests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly AppDbContext Context;

        public TestCommandBase()
        {
            Context = DBContextFactory.Create();
        }

        public void Dispose()
        {
            DBContextFactory.Destroy(Context);
        }
    }
}
