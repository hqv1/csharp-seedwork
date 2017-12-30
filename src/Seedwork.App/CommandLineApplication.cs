using System;
using System.Diagnostics;
using System.Text;

namespace Hqv.Seedwork.App
{
    /// <summary>
    /// Command line application helper
    /// </summary>
    public class CommandLineApplication
    {
        /// <summary>
        /// Settings
        /// </summary>
        public class Settings
        {            
            public Settings(bool displayOutputOnConsole)
            {
                DisplayOutputOnConsole = displayOutputOnConsole;

            }

            /// <summary>
            /// Display error and output information on console window
            /// </summary>
            public bool DisplayOutputOnConsole { get; }
        }

        private readonly StringBuilder _errorBuilder = new StringBuilder();
        private readonly StringBuilder _outputBuilder = new StringBuilder();
        private readonly Settings _settings;

        public CommandLineApplication(Settings settings = null)
        {
            _settings = settings ?? new Settings(false);
        }

        /// <summary>
        /// Run an application in the command line. Does not handle any exceptions.
        /// </summary>
        /// <param name="appPath">Path to the application. Can be just the file name (e.g. ping.exe) if the path is known</param>
        /// <param name="appArguments">Arguments for the application</param>
        /// <returns>Result from the output and error streams.</returns>
        public ProcessResult Run(string appPath, string appArguments)
        {
            _errorBuilder.Clear();
            _outputBuilder.Clear();

            var process = new Process
            {
                StartInfo = CreateProcessStartInfo(appPath, appArguments)
            };
            process.OutputDataReceived += OutputHandler;
            process.ErrorDataReceived += ErrorHandler;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            var output = _outputBuilder.ToString().Trim();            
            var error = _errorBuilder.ToString().Trim();            

            return new ProcessResult(output, error);
        }

        private static ProcessStartInfo CreateProcessStartInfo(string appPath, string appArguments)
        {
            return new ProcessStartInfo
            {
                FileName = appPath,
                Arguments = appArguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
        }

        private void ErrorHandler(object sender, DataReceivedEventArgs e)
        {
            if (_settings.DisplayOutputOnConsole)
            {
                Console.WriteLine(e.Data);
            }
            _errorBuilder.AppendLine(e.Data);
        }

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (_settings.DisplayOutputOnConsole)
            {
                Console.WriteLine(e.Data);
            }
            _outputBuilder.AppendLine(e.Data);
        }
    }
}
