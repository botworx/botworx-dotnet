using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace Botworx.Mia.Runtime
{
    [Flags]
    public enum ControlFlags : int
    {
        None = 0,
        PartialSuccess = 1,
        PartialFailure = 2,
        BranchSuccess = 4,
        Cloned = 8
    }

    public class Method : MentalTask
    {
        protected ControlFlags bwxControlFlags = ControlFlags.None;
        public Method(Process process, Message message)
            : base(process, message)
        {
        }
        public override void OnFinished()
        {
            if (Message.Result == null || Message.Result.Caller == null)
                return;
            //else
            Message.Result.Status = Status;
            Message.Result.Caller.OnReply(this);
        }
        public override void OnReply(Task _task)
        {
            MentalTask task = (MentalTask)_task;
            if (Context != task.Context)
            {
                Method clonedTask = (Method)MemberwiseClone();
                clonedTask.Process = task.Process;
                clonedTask.Context = task.Context;
                //
                IEnumerator<TaskStatus> proc = Procedure;
                Type iterType = proc.GetType();
                MethodInfo procCloner = iterType.GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
                FieldInfo taskField = iterType.GetField("bwxTask");
                IEnumerator<TaskStatus> clonedProc = (IEnumerator<TaskStatus>)procCloner.Invoke(proc, null);
                taskField.SetValue(clonedProc, clonedTask);
                clonedTask.Procedure = clonedProc;
                clonedTask.Schedule();
                return;
            }
            //else
            Schedule();
        }
    }
}
