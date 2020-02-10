using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public delegate void ProcessCreated(Process process);

    public class Brain : Expert
    {
        public Guid Guid;
        public Scheduler Scheduler = new Scheduler();
        public List<Process> Processes = new List<Process>();
        public ProcessCreated ProcessCreated;
        //
        public Brain()
        {
            Guid = Guid.NewGuid();
            ProcessCreated = OnProcessCreated;
        }
        protected Process CreateProcess()
        {
            Process process = Process.Create(this);
            return process;
        }
        public void AddProcess(Process process)
        {
            Processes.Add(process);
        }
        public void RemoveProcess(Process process)
        {
            Processes.Remove(process);
        }
        public void OnProcessCreated(Process process)
        {
        }
        public void Run()
        {
            Process process = CreateProcess();
            AddProcess(process);
            Boot(process, this, new Message());
            Scheduler.Run();
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class BrainAttribute : System.Attribute
    {
        public readonly string Name;
        //
        public BrainAttribute(string name)
        {
            this.Name = name;
        }
    }
}
