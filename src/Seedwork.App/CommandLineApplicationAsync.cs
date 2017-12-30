using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hqv.Seedwork.App
{
    /// <summary>
    /// Run command line application async. Able to cancel and check on progress.
    /// 
    /// https://stackoverflow.com/questions/10788982/is-there-any-async-equivalent-of-process-start/10789196
    /// </summary>
    public class CommandLineApplicationAsync
    {        
        private Process _process;
        private readonly StringBuilder _standardErrorBuilder = new StringBuilder();
        private readonly TaskCompletionSource<string> _standardErrorResults = new TaskCompletionSource<string>();
        private readonly StringBuilder _standardOutputBuilder = new StringBuilder();
        private readonly TaskCompletionSource<string> _standardOutputResults = new TaskCompletionSource<string>();        
        private TaskCompletionSource<ProcessResult> _taskCompletionSource;
        private IProgress<ProcessProgress> _progress;

        public Task<ProcessResult> RunAsync(
            string appPath, 
            string appArguments, 
            IProgress<ProcessProgress> progress = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            _progress = progress;
            _taskCompletionSource = new TaskCompletionSource<ProcessResult>();

            if (cancellationToken.IsCancellationRequested)
            {                
                _taskCompletionSource.SetCanceled();
                return _taskCompletionSource.Task;                
            }
                        
            CreateProcess(appPath, appArguments);            
            var started = _process.Start();
            if (!started)
            {                
                throw new InvalidOperationException("Could not start process" );
            }
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();

            cancellationToken.Register(CancellationRequested);

            return _taskCompletionSource.Task;
        }
        
        private void CancellationRequested()
        {            
            if (_process.HasExited) return;
            try
            {
                _process.Kill();
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void CreateProcess(string appPath, string appArguments)
        {
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = appPath,
                    Arguments = appArguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };

            _process.OutputDataReceived += OutputHandler;
            _process.ErrorDataReceived += ErrorHandler;
            _process.Exited += OnProcessExited;
        }

        private void ErrorHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                _progress?.Report(new ProcessProgress { ErrorData = e.Data });
                _standardErrorBuilder.AppendLine(e.Data);
            }
            else
                _standardErrorResults.SetResult(_standardErrorBuilder.ToString());
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            _taskCompletionSource.TrySetResult(new ProcessResult(
                _standardOutputResults.Task.Result,
                _standardErrorResults.Task.Result,
                _process.ExitCode));
            _process.Dispose();
        }

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                _progress?.Report(new ProcessProgress {OutputData = e.Data});
                _standardOutputBuilder.AppendLine(e.Data);
            }                
            else
                _standardOutputResults.SetResult(_standardOutputBuilder.ToString());
        }        
    }
}