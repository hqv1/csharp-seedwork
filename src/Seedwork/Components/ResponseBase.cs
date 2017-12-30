using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Hqv.Seedwork.Components
{
    /// <summary>
    /// Response base.
    /// 
    /// todo: not a fan of using the [JsonIgnore] attributes. Try rolling our own ContractResolver
    ///     
    /// </summary>
    public abstract class ResponseBase
    {        
        private readonly ConcurrentBag<Exception> _errors = new ConcurrentBag<Exception>();
        private readonly ConcurrentBag<WarningMessage> _warnings = new ConcurrentBag<WarningMessage>();

        protected ResponseBase(RequestBase request)
        {
            Request = request;
        }

        public RequestBase Request { get; }
        public string StatusName { get; set; }
        public object StatusDetail { get; set; }
        public bool IsValid => !_errors.Any();
        [JsonIgnore]
        public IEnumerable<Exception> Errors => _errors;
        public IEnumerable<WarningMessage> Warnings => _warnings;

        public void AddError(Exception ex)
        {
            _errors.Add(ex);
        }

        public void AddErrors(IEnumerable<Exception> exs)
        {
            foreach (var ex in exs)
            {
                AddError(ex);
            }
        }

        public void AddWarning(WarningMessage msg)
        {
            _warnings.Add(msg);
        }

        public void AddWarnings(IEnumerable<WarningMessage> msgs)
        {
            foreach (var msg in msgs)
            {
                AddWarning(msg);
            }
        }
    }
}