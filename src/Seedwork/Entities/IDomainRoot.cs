using System;
using System.Collections.Generic;
using Hqv.Seedwork.Audit;

namespace Hqv.Seedwork.Entities
{
    /// <summary>
    /// Interface for domain root
    /// </summary>
    public interface IDomainRoot
    {
        string Key { get; }

        string CorrelationId { get; }

        bool IsValid { get; }

        IEnumerable<Exception> Exceptions { get; }
        IEnumerable<WarningMessage> Warnings { get; }
        IEnumerable<IBusinessEvent> BusinessEvents { get; }
    }
}