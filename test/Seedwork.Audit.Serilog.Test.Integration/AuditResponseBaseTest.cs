using System;
using Hqv.Seedwork.Audit.Serilog;
using Hqv.Seedwork.Components;
using Microsoft.Extensions.Options;
using Serilog;
using Xunit;

namespace Seedwork.Audit.Serilog.Test.Integration
{
    public class AuditResponseBaseTest
    {
        private readonly AuditorResponseBase _auditorResponseBase;
        private readonly string _entityName;
        private readonly string _entityKey;
        private readonly string _eventName;
        private readonly RequestTest _requestTest;
        private ResponseTest _responseTest;

        public AuditResponseBaseTest()
        {
            var config = Options.Create(new AuditorResponseBase.Config(true, true));
            var logger = new LoggerConfiguration()
                .WriteTo.Trace().CreateLogger();

            _auditorResponseBase = new AuditorResponseBase(config, logger);

            _entityName = "TestEntity";
            _entityKey = "TestKey";
            _eventName = "TestEvent";

            _requestTest = new RequestTest(Guid.NewGuid().ToString()) { RequestValue1 = "Hello" };
        }

        /// <summary>
        /// Message is sent to output screen. Put a break point and review the error message.
        /// </summary>
        [Fact]
        [Trait("Category", "Integration")]
        public void Should_LogSuccessfulMessage()
        {                      
            _responseTest = new ResponseTest(_requestTest) {ResponseValue1 = 1, StatusName = "Good"};
          
            _auditorResponseBase.AuditSuccess(_entityName, _entityKey, _eventName, _responseTest);
        }

        /// <summary>
        /// Message is sent to output screen. Put a break point and review the error message.
        /// </summary>
        [Fact]
        [Trait("Category", "Integration")]
        public void Should_LogFailedMessage()
        {
            var exception = new Exception("This is bad");
            _responseTest = new ResponseTest(_requestTest) { ResponseValue1 = 1, StatusName = "Bad" };
            _responseTest.AddError(exception);

            _auditorResponseBase.AuditFailure(_entityName, _entityKey, _eventName, _responseTest);
        }
    }

    public class RequestTest : RequestBase
    {
        public RequestTest(string correlationId) : base(correlationId)
        {
        }

        public string RequestValue1 { get; set; }
    }

    public class ResponseTest : ResponseBase
    {
        public ResponseTest(RequestBase request) : base(request)
        {
        }

        public int ResponseValue1 { get; set; }
    }
}
