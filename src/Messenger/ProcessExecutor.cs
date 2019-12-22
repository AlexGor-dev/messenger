using System;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text;

namespace Messenger
{
    public class ProcessExecutor
    {
        public ProcessExecutor(string fileName)
        {
            this.fileName = fileName;
            this.timer = new Timer(this.Run);
            this.info = new ProcessStartInfo()
            {
                FileName = fileName,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                ErrorDialog = false,
            };

        }

        private Timer timer;
        private string fileName;
        private Process process;
        private ProcessStartInfo info;
        private int timeOut = 100;

        public bool IsRunning => process != null;

        private StringBuilder errorData = new StringBuilder(10000);
        public string ErrorData => errorData.ToString();

        private StringBuilder outData = new StringBuilder(10000);
        public string OutData => outData.ToString();

        public string Result
        {
            get
            {
                string error = this.ErrorData;
                if (!string.IsNullOrEmpty(error))
                    return error;
                return this.OutData;
            }
        }

        private ManualResetEvent resetEvent = new ManualResetEvent(false);

        private void Run(object state)
        {
            resetEvent.Set();
        }

        public void Start(string arguments, int timeOut)
        {
            if (IsRunning)
                throw new Exception("is running");

            this.timeOut = timeOut;
            this.errorData.Clear();
            this.outData.Clear();

            this.resetEvent.Reset();

            this.info.Arguments = arguments;
            this.process = new Process();
            this.process.ErrorDataReceived += Process_ErrorDataReceived;
            this.process.OutputDataReceived += Process_OutputDataReceived;
            this.process.Exited += Process_Exited;
            this.process.StartInfo = info;
            this.process.Start();
            this.process.BeginErrorReadLine();
            this.process.BeginOutputReadLine();
        }

        public void Start(string argument)
        {
            this.Start(argument, this.timeOut);
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            resetEvent.Set();
            this.process = null;
        }

        void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
                outData.Append(e.Data + Environment.NewLine);
            this.timer.Change(timeOut, -1);
        }


        void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(!string.IsNullOrEmpty(e.Data))
                errorData.Append(e.Data + Environment.NewLine);
            this.timer.Change(timeOut, -1);
        }

        public void SendCmd(string cmd, int timeOut)
        {
            if (!IsRunning)
                throw new Exception("not running");
            this.timer.Change(-1, -1);
            this.timeOut = timeOut;
            this.errorData.Clear();
            this.outData.Clear();
            this.resetEvent.Reset();
            this.process.StandardInput.Write(cmd + Environment.NewLine);
            this.resetEvent.WaitOne();
            this.Wait();
        }

        public void Wait()
        {
            this.resetEvent.WaitOne();
        }

        public void Exit()
        {
            if(IsRunning)
            {
                if (!this.process.HasExited)
                {
                    this.process.Kill();
                    this.process.WaitForExit();
                }
                this.process = null;
            }
        }
    }
}
