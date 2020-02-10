using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Botworx.Mia.Compile
{
    public class Builder
    {
        public static Builder Instance;
        //
        string CurrentDir;
        string WorkingDirectory;
        static string FrameworkDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        string MsBuild = FrameworkDir + "/MsBuild.exe";
        string LogFile;
        //
        public Builder()
        {
            Instance = this;
        }
        public void BuildDirectory(string path)
        {
            BuildStart();
            string[] inputFiles = Directory.GetFiles(path, "*.bws");
            foreach (string inputFile in inputFiles)
            {
                Compile(inputFile);
            }
            BuildFinish();
        }
        public void BuildFiles(string[] paths)
        {
            foreach (var path in paths)
            {
                BuildFile(path);
            }
        }
        public void BuildFiles(string directory, string[] paths)
        {
            foreach (var path in paths)
            {
                BuildFile(directory, path);
            }
        }
        public void BuildFile(string directory, string path)
        {
            BuildFile(directory + path);
        }
        public void BuildFile(string path){
            BuildStart();
            Compile(path);
            BuildFinish();
        }
        private void Compile(string path)
        {
            Compiler compiler = new Compiler();
            compiler.Compile(path);
        }
        public void BuildStart()
        {
            CurrentDir = Environment.CurrentDirectory;
            WorkingDirectory = CurrentDir + "/../../../BwMiaSamples";
            LogFile = WorkingDirectory + "/bwbuild.log";
            LoggingStart(LogFile);
        }
        public void BuildFinish()
        {
            ProcessStartInfo info = new ProcessStartInfo(MsBuild, "BwBrainTest.csproj");
            info.WorkingDirectory = WorkingDirectory;
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            //info.UserName = "UserName";

            using (Process install = Process.Start(info))
            {
                string output = install.StandardOutput.ReadToEnd();
                install.WaitForExit();
#if DEBUG_OUTPUT
                Debug.WriteLine(output);
#endif
            }
            LoggingFinish();
        }
        /////////////////////
#if DEBUG
        private static FileStream LogStream;
#endif
        //
        public void LoggingStart(string filename)
        {
#if DEBUG
            if (LogStream != null)
                return;
            LogStream = new FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            TextWriterTraceListener fileWriter = new TextWriterTraceListener(LogStream);
            Debug.Listeners.Add(fileWriter);
            TextWriterTraceListener consoleWriter = new TextWriterTraceListener(Console.Error);
            Debug.Listeners.Add(consoleWriter);
            Debug.Print(filename);
#endif
        }
        public void LoggingFinish()
        {
#if DEBUG
            Debug.Flush();
            if (LogStream != null)
                LogStream.Close();
#endif
        }
    }
}
