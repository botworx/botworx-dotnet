using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace Botworx.Mia.Runtime
{
    public enum TaskStatus
    {
        Active,
        Stalled,
        Suspended,
        Succeeded,
        Failed,
        Halted,
        Canceled
    }

    public class TaskResult
    {
        public Task Caller;
        public TaskStatus Status;
        public object Value;
        //
        public TaskResult()
        {
        }
        public TaskResult(Task caller)
        {
            Caller = caller;
        }
    }

    public abstract class Task
    {
        public Func<IEnumerator<TaskStatus>> ResetFunc;
        public IEnumerator<TaskStatus> Procedure;
        public TaskStatus Status;
        //
        public bool IsFinished { 
            get { 
                return Status == TaskStatus.Succeeded ||
                    Status == TaskStatus.Failed ||
                    Status == TaskStatus.Halted ||
                    Status == TaskStatus.Canceled; 
            } 
        }
        public bool IsActive { get { return Status == TaskStatus.Active; } }
        public bool IsHalted { get { return Status == TaskStatus.Halted; } }
        public bool IsStalled { get { return Status == TaskStatus.Stalled; } }
        //
        public Task()
        {
        }
        public void Reset()
        {
            if(ResetFunc != null)
                Procedure = ResetFunc();
        }
        public void MaybeSchedule()
        {
            if (!NeedsSchedule())
                return;
            //else
            Schedule();
        }
        public virtual bool NeedsSchedule()
        {
            return true;
        }
        public abstract void Schedule(Scheduler scheduler);
        public abstract void Schedule();
        public virtual void OnFinished()
        {
        }
        public virtual void OnReply(Task task)
        {
        }
        //
        public abstract void Halt();
    }

    public class Task<TScheduler> : Task where TScheduler : Scheduler
    {
        public TScheduler Process;
        //
        public Task()
        {
        }
        public Task(TScheduler scheduler)
        {
            Process = scheduler;
        }
        //
        public override void Schedule(Scheduler scheduler)
        {
            Process = (TScheduler)scheduler;
            Schedule();
        }
        public override void Schedule()
        {
            Reset();
            Process.ScheduleTask(this, this.Procedure);
        }
        public override void Halt()
        {
            Status = TaskStatus.Halted;
            if (Process != null)
                Process.Halt();
        }
    }

    public class BasicTask : Task<Scheduler>
    {
        public BasicTask()
        {
        }
    }
}
