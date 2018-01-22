using Hqv.Seedwork.Components;
using Microsoft.Extensions.Options;
using Serilog;

namespace Hqv.Seedwork.Audit.Serilog
{
    public class AuditorResponseBase : IAuditorResponseBase
    {
        private readonly Config _config;
        private readonly ILogger _logger;

        public class Config
        {
            public const string ConfigurationSectionName = "AuditorResponseBase";

            public Config()
            {
                
            }

            public Config(bool shouldAuditOnSuccessfulEvent, bool shouldDetailAuditOnSuccessfulEvent)
            {
                ShouldAuditOnSuccessfulEvent = shouldAuditOnSuccessfulEvent;
                ShouldDetailAuditOnSuccessfulEvent = shouldDetailAuditOnSuccessfulEvent;
            }

            public bool ShouldAuditOnSuccessfulEvent { get; set; }
            public bool ShouldDetailAuditOnSuccessfulEvent { get; set; }
        }
       
        public AuditorResponseBase(IOptions<Config> config, ILogger logger)
        {
            _logger = logger;
            _config = config.Value;
        }

        public void AuditFailure(IBusinessEvent businessEvent)
        {
            _logger.Error("{@BusinessEvent} failed", businessEvent);
        }

        public void AuditSuccess(IBusinessEvent businessEvent)
        {
            _logger.Information("{@BusinessEvent} succeeded", businessEvent);
        }

        public void AuditSuccess(string entityName, string entityKey, string eventName, ResponseBase response, int version = 1)
        {
            if (!_config.ShouldAuditOnSuccessfulEvent)
                return;

            var businessEvent = new BusinessEvent(
                entityName: entityName,
                entityKey: entityKey,
                eventName: eventName,
                correlationId: response.Request?.CorrelationId,
                version: version,
                entityObject: _config.ShouldDetailAuditOnSuccessfulEvent ? response : null);

            AuditSuccess(businessEvent);
        }

        public void AuditFailure(string entityName, string entityKey, string eventName, ResponseBase response, int version = 1)
        {
            var businessEvent = new BusinessEvent(
                entityName: entityName,
                entityKey: entityKey,
                eventName: eventName,
                correlationId: response.Request?.CorrelationId,
                version: version,
                entityObject: response);

            AuditFailure(businessEvent);
        }
    }
}
