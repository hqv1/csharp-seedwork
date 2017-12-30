using System;

namespace Hqv.Seedwork.Audit
{
    public class BusinessEventDefault : IBusinessEvent
    {
        public BusinessEventDefault(string entityName, string entityKey, string eventName, 
            DateTime eventDateTime, string correlationId = null, int version = 1, object entityObject = null, object additionalMetadata = null)
        {
            CorrelationId = correlationId;
            EntityName = entityName;
            EntityKey = entityKey;
            EventName = eventName;
            Version = version;
            EventDateTime = eventDateTime;
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