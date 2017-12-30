namespace Hqv.Seedwork.App
{
    public class ProcessResult
    {
        public ProcessResult(string outputData, string errorData, int resultCode = 0)
        {
            ResultCode = resultCode;
            OutputData = outputData;
            ErrorData = errorData;
        }

        public int ResultCode { get;}
        public string OutputData { get; }
        public string ErrorData { get; }
    }
}