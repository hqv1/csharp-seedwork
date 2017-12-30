namespace Hqv.Seedwork.Audit
{
    /// <summary>
    /// Auditor stores business events
    /// </summary>
    public interface IAuditor
    {
        void AuditSuccess(IBusinessEvent businessEvent);

        void AuditFailure(IBusinessEvent businessEvent);
    }
}