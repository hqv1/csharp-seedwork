using System;

namespace Hqv.Seedwork.Audit
{
    /// <summary>
    /// A business event
    /// </summary>
    public interface IBusinessEvent
    {
        string CorrelationId { get; set; }
        string EntityName { get; }
        string EntityKey { get; }
        string EventName { get; }
        int Version { get; }
        DateTime EventDateTime { get; }
        object EntityObject { get; set; }
        object AdditionalMetadata { get; set; }
    }
}