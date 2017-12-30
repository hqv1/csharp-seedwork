using System;
using Hqv.Seedwork.Entities;

namespace Hqv.Seedwork.Audit
{
    public abstract class BusinessEventDomainRootBase : IBusinessEvent
    {
        protected BusinessEventDomainRootBase(string entityName, int version, IDomainRoot domainRoot, string eventName, object entityObject = null, object additionalMetadata = null)
        {
            EntityKey = domainRoot.Key;
            CorrelationId = domainRoot.CorrelationId;
            EventName = eventName;
            EntityObject = entityObject;
            AdditionalMetadata = additionalMetadata;

            EntityName = entityName;
            Version = version;
            EventDateTime = DateTime.Now;
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