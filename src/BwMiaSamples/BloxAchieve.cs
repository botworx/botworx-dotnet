using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Botworx.Mia;
using Botworx.Mia.Runtime;

namespace BwBrainTest
{
    [Brain("BloxAchieveBrain")]
    public class BloxAchieveBrain : Brain
    {
        public BloxAchieveBrain()
        {
            Boot = Create;
            AddTrigger(new Trigger(BloxAchieveBrain.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_bloxAchieveBrain, null, MatchFlag.None))));
            AddTrigger(new Trigger(BloxAchieve.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_blox, null, MatchFlag.None))));
        }
        public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
        {
            BloxAchieveBrain _bwxExpert = new BloxAchieveBrain();
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
            Ent_BloxAchieveContext = new Entity("BloxAchieveContext", _bwxContext);
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Table1, Ent_isClear, true));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Block1, Ent_isClear, true));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Block1, Ent_onTop, Ent_Table1));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Block2, Ent_isClear, true));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Block2, Ent_onTop, Ent_Block1));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Block3, Ent_isClear, true));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Block3, Ent_onTop, Ent_Block2));
            _bwxContext.Add(new Clause(Ent_Achieve, Ent_Block1, Ent_on, Ent_Block2));
            _bwxContext.Add(new Clause(Ent_Achieve, Ent_Block2, Ent_on, Ent_Block3));
            _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_blox, null){ { Ent_context, Ent_BloxAchieveContext } });
            Post(bwxTask.Process, _bwxMsg);
            yield return Succeed(bwxTask.Process, bwxTask.Message);
        }
        public class BloxAchieve : Expert
        {
            public BloxAchieve()
            {
                Boot = Create;
                AddTrigger(new Trigger(BloxAchieve.Impasse, MessagePattern.NilMessagePattern));
                AddTrigger(new Trigger(BloxAchieve.GoalElab, new MessagePattern(MessageKind.Add, new ClausePattern(Ent_Goal, null, null, null, MatchFlag.SubjectX | MatchFlag.PredicateX | MatchFlag.ObjectX))));
                AddTrigger(new Trigger(BloxAchieve.NotGoalElab, new MessagePattern(MessageKind.Remove, new ClausePattern(Ent_Goal, null, null, null, MatchFlag.SubjectX | MatchFlag.PredicateX | MatchFlag.ObjectX))));
                AddTrigger(new Trigger(BloxAchieve.AchieveXonY, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Achieve, null, Ent_on, null, MatchFlag.SubjectX | MatchFlag.ObjectX))));
                AddTrigger(new Trigger(BloxAchieve.Stack, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_stack, null, MatchFlag.ObjectX))));
                AddTrigger(new Trigger(BloxAchieve.Clear, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_clear, null, MatchFlag.ObjectX))));
                AddTrigger(new Trigger(BloxAchieve.NotOnTopElab, new MessagePattern(MessageKind.Remove, new ClausePattern(Ent_Belief, null, Ent_onTop, null, MatchFlag.SubjectX | MatchFlag.ObjectX))));
                AddTrigger(new Trigger(BloxAchieve.OntopElab, new MessagePattern(MessageKind.Add, new ClausePattern(Ent_Belief, null, Ent_onTop, null, MatchFlag.SubjectX | MatchFlag.ObjectX))));
            }
            public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                BloxAchieve _bwxExpert = new BloxAchieve();
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
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void Impasse(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).ImpasseProc(bwxTask));
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
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).GoalElabProc(bwxTask, g));
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
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).NotGoalElabProc(bwxTask, g));
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
            public static void AchieveXonY(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom g = bwxMsg.Clause;
                Atom x = bwxMsg.Clause.Subject;
                Atom y = (Atom)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).AchieveXonYProc(bwxTask, g, x, y));
            }
            public IEnumerator<TaskStatus> AchieveXonYProc(MentalTask bwxTask, Atom g, Atom x, Atom y)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_stack, x){ { Ent_on, y } });
                Post(bwxTask.Process, _bwxMsg);
                _bwxMsg = new Message(MessageKind.Remove, null, null, (Clause)g);
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void Stack(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom x = (Atom)bwxMsg.Clause.Object;
                Atom y = (Atom)bwxMsg.Clause[Ent_on];
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).StackProc(bwxTask, x, y));
            }
            public IEnumerator<TaskStatus> StackProc(MentalTask bwxTask, Atom x, Atom y)
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
                        if(!bwxTask.Context.Exists(x, Ent_isClear, true))
                        {
                            _bwxQflags |= ControlFlags.PartialSuccess;
                            {
                                _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_clear, x));
                                Post(bwxTask.Process, _bwxMsg);
                            }
                        }
                        else
                        {
                            _bwxQflags |= ControlFlags.PartialFailure;
                        }
                    }
                    if(_bwxQflags == ControlFlags.PartialSuccess)
                    {
                        {
                            yield return Fail(bwxTask.Process, bwxTask.Message);
                        }
                    }
                }
                {
                    ControlFlags _bwxQflags = ControlFlags.None;
                    {
                        if(!bwxTask.Context.Exists(y, Ent_isClear, true))
                        {
                            _bwxQflags |= ControlFlags.PartialSuccess;
                            {
                                _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_clear, y));
                                Post(bwxTask.Process, _bwxMsg);
                            }
                        }
                        else
                        {
                            _bwxQflags |= ControlFlags.PartialFailure;
                        }
                    }
                    if(_bwxQflags == ControlFlags.PartialSuccess)
                    {
                        {
                            yield return Fail(bwxTask.Process, bwxTask.Message);
                        }
                    }
                }
                {
                    foreach(Atom z in bwxTask.Context.QuerySubjPred<Atom>(x, Ent_onTop))
                    {
                        {
                            _bwxMsg = new Message(MessageKind.Remove, null, null, new Clause(Ent_Belief, x, Ent_onTop, z));
                            Post(bwxTask.Process, _bwxMsg);
                        }
                    }
                }
                _bwxMsg = new Message(MessageKind.Add, null, null, new Clause(Ent_Belief, x, Ent_onTop, y));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void Clear(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom x = (Atom)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).ClearProc(bwxTask, x));
            }
            public IEnumerator<TaskStatus> ClearProc(MentalTask bwxTask, Atom x)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                {
                    foreach(Atom y in bwxTask.Context.QuerySubjPred<Atom>(x, Ent_beneath))
                    {
                        foreach(Atom z in bwxTask.Context.QueryPredObj(Ent_isClear, true))
                        {
                            if(z != x)
                            {
                                if(z != y)
                                {
                                    {
                                        _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_stack, y){ { Ent_on, z } });
                                        Propose(bwxTask.Process, _bwxMsg, null);
                                    }
                                }
                            }
                        }
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void NotOnTopElab(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom x = bwxMsg.Clause.Subject;
                Atom y = (Atom)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).NotOnTopElabProc(bwxTask, x, y));
            }
            public IEnumerator<TaskStatus> NotOnTopElabProc(MentalTask bwxTask, Atom x, Atom y)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                _bwxMsg = new Message(MessageKind.Remove, null, null, new Clause(Ent_Belief, y, Ent_beneath, x));
                Post(bwxTask.Process, _bwxMsg);
                _bwxMsg = new Message(MessageKind.Add, null, null, new Clause(Ent_Belief, y, Ent_isClear, true));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void OntopElab(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom x = bwxMsg.Clause.Subject;
                Atom y = (Atom)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((BloxAchieve)bwxExpert).OntopElabProc(bwxTask, x, y));
            }
            public IEnumerator<TaskStatus> OntopElabProc(MentalTask bwxTask, Atom x, Atom y)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                {
                    if(y.TypeCheck(Ent_Block) && bwxTask.Context.Exists(y, Ent_isClear, true))
                    {
                        {
                            _bwxMsg = new Message(MessageKind.Remove, null, null, new Clause(Ent_Belief, y, Ent_isClear, true));
                            Post(bwxTask.Process, _bwxMsg);
                        }
                    }
                }
                {
                    if(bwxTask.Context.Exists(x, Ent_onTop, y))
                    {
                        {
                            _bwxMsg = new Message(MessageKind.Add, null, null, new Clause(Ent_Belief, y, Ent_beneath, x));
                            Post(bwxTask.Process, _bwxMsg);
                        }
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
        }
        public static AtomType Ent_isClear = new AtomType("isClear", Ent_Entity);
        public static Entity Ent_BloxAchieveContext = new Entity("BloxAchieveContext", Ent_Entity);
        public static AtomType Ent_Table = new AtomType("Table", Ent_Entity);
        public static Entity Ent_Table1 = new Entity("Table1", Ent_Table);
        public static AtomType Ent_Block = new AtomType("Block", Ent_Entity);
        public static Entity Ent_Block1 = new Entity("Block1", Ent_Block);
        public static AtomType Ent_onTop = new AtomType("onTop", Ent_Entity);
        public static Entity Ent_Block2 = new Entity("Block2", Ent_Block);
        public static Entity Ent_Block3 = new Entity("Block3", Ent_Block);
        public static AtomType Ent_on = new AtomType("on", Ent_Entity);
        public static AtomType Ent_blox = new AtomType("blox", Ent_Entity);
        public static AtomType Ent_bloxAchieveBrain = new AtomType("bloxAchieveBrain", Ent_Entity);
        public static Entity Ent_Active = new Entity("Active");
        public static AtomType Ent_stack = new AtomType("stack", Ent_Entity);
        public static AtomType Ent_clear = new AtomType("clear", Ent_Entity);
        public static AtomType Ent_beneath = new AtomType("beneath", Ent_Entity);
    }
}
