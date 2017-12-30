using FluentAssertions;
using Xunit;

namespace Hqv.Seedwork.App.Test.Integration
{
    public class CommandLineApplicationTest
    {
        private readonly CommandLineApplication _app;
        private string _appPath;
        private string _appArguments;
        private ProcessResult _result;

        public CommandLineApplicationTest()
        {
            _app = new CommandLineApplication();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Should_RunApp_Successfully()
        {
            GivenAValidSetup();
            _result = _app.Run(_appPath, _appArguments);
            _result.ErrorData.Should().BeNullOrEmpty();
            _result.OutputData.Should().Contain("yahoo.com");
        }

        private void GivenAValidSetup()
        {
            _appPath = "ping.exe";
            _appArguments = "yahoo.com";
        }
    }
}
