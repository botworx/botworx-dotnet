using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Botworx.Mia;
using Botworx.Mia.Runtime;

namespace BwBrainTest
{
    [Brain("CountingBrain")]
    public class CountingBrain : Brain
    {
        public CountingBrain()
        {
            Boot = Create;
            AddTrigger(new Trigger(CountingBrain.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_countingBrain, null, MatchFlag.None))));
            AddTrigger(new Trigger(Counting.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_counting, null, MatchFlag.None))));
        }
        public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
        {
            CountingBrain _bwxExpert = new CountingBrain();
            bwxProcess.PushExpert(_bwxExpert);
            MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
            bwxProcess.ScheduleTask(bwxTask, _bwxExpert.CreateProc(bwxTask));
        }
        public IEnumerator<TaskStatus> CreateProc(MentalTask bwxTask)
        {
            Task _bwxTask = null;
            Context _bwxContext = null;
            Message _bwxMsg;
            Clause _bwxClause = null;
            Atom _bwxSubject = null;
            TaskResult _bwxResult = null;
            Begin(bwxTask);
            _bwxContext = new Context();
            Ent_CountingContext = new Entity("CountingContext", _bwxContext);
            _bwxSubject = _bwxContext.Add(new Clause(Ent_Perform, Ent_Self, Ent_countTo, 5));
            _bwxContext.Add(new Clause(Ent_Belief, _bwxSubject, Ent_value, 0));
            _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_counting, null){ { Ent_context, Ent_CountingContext } });
            Propose(bwxTask.Process, _bwxMsg, null);
            yield return Succeed(bwxTask.Process, bwxTask.Message);
        }
        public class Counting : Deliberator
        {
            public Counting()
            {
                Boot = Create;
                AddTrigger(new Trigger(Counting.Impasse, MessagePattern.NilMessagePattern));
                AddTrigger(new Trigger(Counting.GoalElab, new MessagePattern(MessageKind.Add, new ClausePattern(Ent_Goal, null, null, null, MatchFlag.SubjectX | MatchFlag.PredicateX | MatchFlag.ObjectX))));
                AddTrigger(new Trigger(Counting.NotGoalElab, new MessagePattern(MessageKind.Remove, new ClausePattern(Ent_Goal, null, null, null, MatchFlag.SubjectX | MatchFlag.PredicateX | MatchFlag.ObjectX))));
                AddTrigger(new Trigger(Counting.IncrementPropose, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_countTo, null, MatchFlag.ObjectX))));
                AddTrigger(new Trigger(Counting.IncrementApply, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_increment, null, MatchFlag.ObjectX))));
            }
            public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Counting _bwxExpert = new Counting();
                bwxProcess.PushExpert(_bwxExpert);
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, _bwxExpert.CreateProc(bwxTask));
            }
            public IEnumerator<TaskStatus> CreateProc(MentalTask bwxTask)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                Console.WriteLine("Counting in sub-contexts");
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void Impasse(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Counting)bwxExpert).ImpasseProc(bwxTask));
            }
            public IEnumerator<TaskStatus> ImpasseProc(MentalTask bwxTask)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                {
                    ControlFlags _bwxQflags = ControlFlags.None;
                    {
                        foreach(Atom g in bwxTask.Context.QueryType(Ent_Goal))
                        {
                            if(bwxTask.Context.Exists(g, Ent_status, Ent_Active))
                            {
                                _bwxQflags |= ControlFlags.PartialSuccess;
                                {
                                    _bwxMsg = new Message(MessageKind.Attempt, null, null, (Clause)g);
                                    Propose(bwxTask.Process, _bwxMsg, null);
                                }
                            }
                            else
                            {
                                _bwxQflags |= ControlFlags.PartialFailure;
                            }
                        }
                    }
                    if(_bwxQflags == ControlFlags.None || _bwxQflags == ControlFlags.PartialFailure)
                    {
                        {
                            bwxTask.Process.Halt();
                        }
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void GoalElab(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom g = bwxMsg.Clause;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Counting)bwxExpert).GoalElabProc(bwxTask, g));
            }
            public IEnumerator<TaskStatus> GoalElabProc(MentalTask bwxTask, Atom g)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                _bwxMsg = new Message(MessageKind.Add, null, null, new Clause(Ent_Belief, g, Ent_status, Ent_Active));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void NotGoalElab(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom g = bwxMsg.Clause;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Counting)bwxExpert).NotGoalElabProc(bwxTask, g));
            }
            public IEnumerator<TaskStatus> NotGoalElabProc(MentalTask bwxTask, Atom g)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                _bwxMsg = new Message(MessageKind.Remove, null, null, new Clause(Ent_Belief, g, Ent_status, Ent_Active));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void IncrementPropose(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom g = bwxMsg.Clause;
                int v1 = (int)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Counting)bwxExpert).IncrementProposeProc(bwxTask, g, v1));
            }
            public IEnumerator<TaskStatus> IncrementProposeProc(MentalTask bwxTask, Atom g, int v1)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                {
                    foreach(int v2 in bwxTask.Context.QuerySubjPred<int>(g, Ent_value))
                    {
                        if(v1 == v2)
                        {
                            {
                                yield return Succeed(bwxTask.Process, bwxTask.Message);
                            }
                        }
                    }
                }
                _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_increment, g));
                Propose(bwxTask.Process, _bwxMsg, null);
                yield return Return(bwxTask.Process, bwxTask.Message);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void IncrementApply(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom g = (Atom)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Counting)bwxExpert).IncrementApplyProc(bwxTask, g));
            }
            public IEnumerator<TaskStatus> IncrementApplyProc(MentalTask bwxTask, Atom g)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                {
                    foreach(int v1 in bwxTask.Context.QuerySubjPred<int>(g, Ent_value))
                    {
                        {
                            _bwxMsg = new Message(MessageKind.Remove, null, null, new Clause(Ent_Belief, g, Ent_value, v1));
                            Post(bwxTask.Process, _bwxMsg);
                            var v2 = v1 + 1;
                            _bwxMsg = new Message(MessageKind.Add, null, null, new Clause(Ent_Belief, g, Ent_value, v2));
                            Post(bwxTask.Process, _bwxMsg);
                            Console.WriteLine("{0} ...", v2);
                        }
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
        }
        public static AtomType Ent_countTo = new AtomType("countTo", Ent_Entity);
        public static AtomType Ent_achieved = new AtomType("achieved", Ent_Entity);
        public static AtomType Ent_value = new AtomType("value", Ent_Entity);
        public static Entity Ent_CountingContext = new Entity("CountingContext", Ent_Entity);
        public static AtomType Ent_counting = new AtomType("counting", Ent_Entity);
        public static AtomType Ent_countingBrain = new AtomType("countingBrain", Ent_Entity);
        public static Entity Ent_Active = new Entity("Active");
        public static AtomType Ent_increment = new AtomType("increment", Ent_Entity);
    }
}
