using System;
using Hqv.Seedwork.Components;

namespace Hqv.Seedwork.Audit.Serilog
{
    public class BusinessEvent : IBusinessEvent
    {
        public BusinessEvent(string entityName, string entityKey, string eventName, string correlationId, int version = 1, ResponseBase entityObject = null, object additionalMetadata = null)
        {
            EntityName = entityName;
            EntityKey = entityKey;
            EventName = eventName;
            CorrelationId = correlationId;
            Version = version;
            EventDateTime = DateTime.Now;
            EntityObject = entityObject;
            AdditionalMetadata = additionalMetadata;
        }

        public string CorrelationId { get; set; }
        public string EntityName { get; }
        public string EntityKey { get; }
        public string EventName { get; }
        public int Version { get; }
        public DateTime EventDateTime { get; }
        public object EntityObject { get; set; }
        public object AdditionalMetadata { get; set; }
    }
}