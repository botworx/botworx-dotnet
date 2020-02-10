using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx
{
    public class TaskScheduler
    {
        TaskScheduler Super;
        protected Queue<Task> TaskQueue = new Queue<Task>();
        bool IsRunning = true;
        protected Queue<Message> Posts = new Queue<Message>();
        //
        public TaskScheduler()
        {
            Init();
        }
        public TaskScheduler(TaskScheduler super)
        {
            Super = super;
            Init();
        }
        void Init()
        {
        }
        //
        public IEnumerator<TaskStatus> Update()
        {
            Run();
            yield return TaskStatus.Success;
        }
        public void Halt()
        {
            IsRunning = false;
            if (Super != null)
                Super.Halt();
        }
        public void Post(Message post)
        {
            bool schedule = NeedsSchedule();
            Posts.Enqueue(post);
            if (schedule)
                Schedule();
        }
        public bool NeedsSchedule()
        {
            if (Posts.Count == 0 && TaskQueue.Count == 0)
                return true;
            else
                return false;
        }
        public void Schedule()
        {
        }
        public void Schedule(Task task, IEnumerator<TaskStatus> procedure)
        {
            task.procedure = procedure;
            task.status = TaskStatus.Active;
            task.scheduler = this;
            TaskQueue.Enqueue(task);
        }
        public void Run()
        {
            Task task = null;
            while (TaskQueue.Count != 0 && IsRunning)
            {
                task = TaskQueue.Dequeue();
                switch (task.Process())
                {
                    case TaskStatus.Active:
                        TaskQueue.Enqueue(task);
                        break;
                    case TaskStatus.Success:
                    case TaskStatus.Failure:
                        task.OnFinished();
                        break;
                }
            }
            if (IsRunning)
                OnStall();
        }
        public void Process()
        {
            int taskCount = TaskQueue.Count;
            Task task = null;
            while (taskCount-- != 0 && IsRunning)
            {
                task = TaskQueue.Dequeue();
                switch (task.Process())
                {
                    case TaskStatus.Active:
                        TaskQueue.Enqueue(task); //TODO: Problems with using Priority Queue?
                        break;
                    case TaskStatus.Success:
                    case TaskStatus.Failure:
                        task.OnFinished();
                        break;
                }
            }
        }
        protected virtual void OnStall()
        {
        }
    }
}
