using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace Hqv.Seedwork.App.Test.Integration
{
    public class CommandLineApplicationAsyncTest
    {
        private readonly CommandLineApplicationAsync _app;
        private string _appPath;
        private string _appArguments;
        private ProcessResult _result;

        public CommandLineApplicationAsyncTest()
        {
            _app = new CommandLineApplicationAsync();
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Should_RunApp_Successfully()
        {
            GivenAValidSetup();
            _result = _app.RunAsync(_appPath, _appArguments).Result;
            _result.ErrorData.Should().BeNullOrEmpty();
            _result.OutputData.Should().Contain("yahoo.com");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Should_RunApp_Successfully_WithProgress()
        {
            var progress = new Progress<ProcessProgress>(p =>
            {
                if (!string.IsNullOrEmpty(p.ErrorData))
                {
                    Debug.WriteLine(p.ErrorData);
                }
                if (!string.IsNullOrEmpty(p.OutputData))
                {
                    Debug.WriteLine(p.OutputData);
                }
            });
            GivenAValidSetup();
            _result = _app.RunAsync(_appPath, _appArguments, progress).Result;
            _result.ErrorData.Should().BeNullOrEmpty();
            _result.OutputData.Should().Contain("yahoo.com");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Should_CancelApp()
        {
            GivenAValidSetup();
            var cancellationTokenSource = new CancellationTokenSource();
            var task = _app.RunAsync(_appPath, _appArguments, cancellationToken: cancellationTokenSource.Token);
            cancellationTokenSource.Cancel();
            var result = task.Result;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Should_CancelAppImmediately()
        {
            GivenAValidSetup();            
            var task = _app.RunAsync(_appPath, _appArguments, cancellationToken: new CancellationToken(true));
            if (!task.IsCanceled)
            {
                var result = task.Result;
            }
        }

        private void GivenAValidSetup()
        {
            _appPath = "ping.exe";
            _appArguments = "yahoo.com";
        }
    }
}