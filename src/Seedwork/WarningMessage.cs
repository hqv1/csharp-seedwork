namespace Hqv.Seedwork
{
    /// <summary>
    /// Message used for warnings
    /// </summary>
    public class WarningMessage
    {
        public WarningMessage(string title, object additionalInfo)
        {
            Title = title;
            AdditionalInfo = additionalInfo;
        }

        public string Title { get; }
        public object AdditionalInfo { get; }
    }
}