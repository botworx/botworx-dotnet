using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace Botworx
{
    public enum TaskStatus
    {
        Active,
        Suspended,
        Success,
        Failure
    }
    public class Task
    {
        public IEnumerator<TaskStatus> procedure;
        public Task parent;
        public TaskScheduler scheduler;
        public TaskStatus status;
        //
        public Task()
        {
            parent = null;
            procedure = null;
            scheduler = null;
        }
        public Task(Task parent)
        {
            this.parent = parent;
            procedure = null;
            scheduler = null;
        }
        public TaskStatus Process()
        {
            procedure.MoveNext();
            return procedure.Current;
        }
        public virtual void OnFinished()
        {
            if (parent != null)
                parent.OnChildFinished(this);
        }
        public virtual void OnChildFinished(Task task)
        {
            scheduler.Schedule(this, this.procedure);
        }
        public void Post(Message msg)
        {
            this.scheduler.Post(msg);
        }
        //
        public void Halt()
        {
            this.scheduler.Halt();
        }
    }
    /*
     * Used for tasks that need to return a value.
     */
    public class TaskT<T> : Task
    {
        public T value;
    }
}
