using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Scheduler : BasicTask
    {
        protected Queue<Task> TaskQueue = new Queue<Task>();
        protected int StallCount = 0;
        public bool CanJumpStart = true;
        //
        public Scheduler()
        {
            this.ResetFunc = () => { return UpdateProc(); };
        }
        //
        public override bool NeedsSchedule()
        {
            if (TaskQueue.Count == 0)
                return true;
            else
                return false;
        }
        public virtual bool NeedsUpdating()
        {
            if (TaskQueue.Count != 0 && IsActive)
                return true;
            else
                return false;
        }
        public void ScheduleTask(Task task, IEnumerator<TaskStatus> procedure)
        {
            task.Procedure = procedure;
            task.Status = TaskStatus.Active;
            TaskQueue.Enqueue(task);
        }
        public IEnumerator<TaskStatus> UpdateProc()
        {
            Run();
            yield return TaskStatus.Succeeded;
        }
        public void Run()
        {
            while (IsActive)
            {
                while (NeedsUpdating() && IsActive)
                {
                    Update();
                }
                if (IsActive)
                    Stall();
            }
        }
        public virtual void Update()
        {
            Task task = null;
            IEnumerator<TaskStatus> iter = null;
            while (TaskQueue.Count != 0 && IsActive)
            {
                task = TaskQueue.Dequeue();
                iter = task.Procedure;
                iter.MoveNext();
                TaskStatus status = iter.Current;
                task.Status = status;
                switch (status)
                {
                    case TaskStatus.Active:
                        task.Schedule();
                        break;
                    case TaskStatus.Suspended:
                        break;
                    case TaskStatus.Succeeded:
                    case TaskStatus.Failed:
                    case TaskStatus.Halted:
                    case TaskStatus.Canceled:
                        task.OnFinished();
                        break;
                }
            }
        }
        public void Stall()
        {
            Status = TaskStatus.Stalled;
            ++StallCount;
            if(CanJumpStart)
                JumpStart();
        }
        protected virtual void JumpStart()
        {
        }
    }
}
