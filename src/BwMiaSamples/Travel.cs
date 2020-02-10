using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Botworx.Mia.Runtime;

namespace BwBrainTest
{
    [Brain("TravelBrain")]
    public class TravelBrain : Brain
    {
        public TravelBrain()
        {
            Boot = Create;
            AddTrigger(new Trigger(Travel.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_travel, null, MatchFlags.None))));
        }
        public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
        {
            TravelBrain _bwxExpert = new TravelBrain();
            bwxProcess.PushExpert(_bwxExpert);
            MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
            bwxProcess.ScheduleTask(bwxTask, _bwxExpert.CreateProc(bwxTask));
        }
        public IEnumerator<TaskStatus> CreateProc(MentalTask bwxTask)
        {
            Task _bwxTask;
            Context _bwxContext;
            Message _bwxMsg;
            Clause _bwxClause;
            Element _bwxSubject;
            TaskResult _bwxResult;
            _bwxContext = new Context();
            Ent_TravelContext = new Entity("TravelContext", _bwxContext);
            _bwxSubject = Ent_Taxi1;
            _bwxSubject = Ent_Home1;
            _bwxClause = _bwxContext.Add(new Clause(Ent_Belief, _bwxSubject, Ent_distanceTo, (Element)(Ent_Restaurant1)));
            _bwxClause[Ent_distance] = (float)8;
            _bwxSubject = Ent_Restaurant1;
            _bwxSubject = Ent_Self;
            _bwxContext.Add(new Clause(Ent_Belief, _bwxSubject, Ent_location, (Element)(Ent_Home1)));
            _bwxContext.Add(new Clause(Ent_Belief, _bwxSubject, Ent_cash, (float)(20.0f)));
            _bwxContext.Add(new Clause(Ent_Perform, Ent_Self, Ent_travelTo, (Element)(Ent_Restaurant1)));
            _bwxResult = new TaskResult(bwxTask);
            _bwxMsg = new Message(MessageKind.Attempt, bwxTask, _bwxResult, new Clause(Ent_Perform, Ent_Self, Ent_travel, (Element)(null)));
            Post(bwxTask.Process, _bwxMsg);
            bwxTask.Process.ImportContext((Context)Ent_TravelContext.Object);
            yield return TaskStatus.Suspended;
            if(_bwxResult.Status != TaskStatus.Succeeded)
                yield return Fail(bwxTask.Process, bwxTask.Message);
            yield return Succeed(bwxTask.Process, bwxTask.Message);
        }
        public class Travel : Expert
        {
            public Travel()
            {
                Boot = Create;
                AddTrigger(new Trigger(Travel.TravelByFoot, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_travelTo, null, MatchFlags.ObjectX))));
                AddTrigger(new Trigger(Travel.Walk, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_walkTo, null, MatchFlags.ObjectX))));
                AddTrigger(new Trigger(Travel.TravelByTaxi, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_travelTo, null, MatchFlags.ObjectX))));
                AddTrigger(new Trigger(Travel.CallTaxi, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_callTaxi, null, MatchFlags.ObjectX))));
                AddTrigger(new Trigger(Travel.Ride, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_ride, null, MatchFlags.None))));
                AddTrigger(new Trigger(Travel.PayDriver, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_payDriver, null, MatchFlags.None))));
                AddTrigger(new Trigger(Travel.Impasse, MessagePattern.NilMessagePattern));
                AddTrigger(new Trigger(Travel.GoalElab, new MessagePattern(MessageKind.Add, new ClausePattern(Ent_Goal, null, null, null, MatchFlags.SubjectX | MatchFlags.PredicateX | MatchFlags.ObjectX))));
                AddTrigger(new Trigger(Travel.NotGoalElab, new MessagePattern(MessageKind.Remove, new ClausePattern(Ent_Goal, null, null, null, MatchFlags.SubjectX | MatchFlags.PredicateX | MatchFlags.ObjectX))));
            }
            public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Travel _bwxExpert = new Travel();
                bwxProcess.PushExpert(_bwxExpert);
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, _bwxExpert.CreateProc(bwxTask));
            }
            public IEnumerator<TaskStatus> CreateProc(MentalTask bwxTask)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void TravelByFoot(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element destination = (Element)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).TravelByFootProc(bwxTask, destination));
            }
            public IEnumerator<TaskStatus> TravelByFootProc(MentalTask bwxTask, Element destination)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                {
                    ControlFlags _bwxQflags = ControlFlags.None;
                    {
                        foreach(Element origin in bwxTask.Context.QuerySubjPred<Element>(Ent_Self, Ent_location))
                        {
                            _bwxClause = bwxTask.Context.Find(new ClausePattern(origin, Ent_distanceTo, destination));
                            if (_bwxClause != null)
                            {
                                if (_bwxClause != null && _bwxClause.Exists<float>(Ent_distance, (x) => x <= 2))
                                {
                                    _bwxQflags |= ControlFlags.PartialSuccess;
                                }
                                else
                                {
                                    _bwxQflags |= ControlFlags.PartialFailure;
                                }
                            }
                        }
                    }
                    if(_bwxQflags == ControlFlags.None || _bwxQflags == ControlFlags.PartialFailure)
                    {
                        yield return Throw(bwxTask.Process, bwxTask.Message);
                    }
                }
                _bwxResult = new TaskResult(bwxTask);
                _bwxMsg = new Message(MessageKind.Attempt, bwxTask, _bwxResult, new Clause(Ent_Perform, Ent_Self, Ent_walkTo, (Element)(destination)));
                Propose(bwxTask.Process, _bwxMsg, null);
                yield return TaskStatus.Suspended;
                if(_bwxResult.Status != TaskStatus.Succeeded)
                    yield return Fail(bwxTask.Process, bwxTask.Message);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void Walk(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element destination = (Element)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).WalkProc(bwxTask, destination));
            }
            public IEnumerator<TaskStatus> WalkProc(MentalTask bwxTask, Element destination)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                _bwxMsg = new Message(MessageKind.Add, bwxTask, null, new Clause(Ent_Belief, Ent_Self, Ent_location, (Element)(destination)));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void TravelByTaxi(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element destination = (Element)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).TravelByTaxiProc(bwxTask, destination));
            }
            public IEnumerator<TaskStatus> TravelByTaxiProc(MentalTask bwxTask, Element destination)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                {
                    ControlFlags _bwxQflags = ControlFlags.None;
                    {
                        foreach(Element origin in bwxTask.Context.QuerySubjPred<Element>(Ent_Self, Ent_location))
                        {
                            _bwxClause = bwxTask.Context.Find(new ClausePattern(origin, Ent_distanceTo, destination));
                            if (_bwxClause != null)
                            {
                                float distance = (float)_bwxClause[Ent_distance];
                                {
                                    if(bwxTask.Context.Exists<float>(Ent_Self, Ent_cash, (x) => x >= 1.5 + .5 * distance))
                                    {
                                        _bwxQflags |= ControlFlags.PartialSuccess;
                                        _bwxMsg = new Message(MessageKind.Callback, bwxTask, null, new Clause(Ent_Clause, Ent_Self, Ent_callback, (MessageCallback)((__bwxContext, __bwxExpert, __bwxMessage) => { Method __bwxTask = new Method(__bwxContext, __bwxMessage); __bwxContext.ScheduleTask(__bwxTask, TravelByTaxi_1Proc(__bwxTask, destination, origin, distance)); })));
                                        Post(bwxTask.Process, _bwxMsg);
                                    }
                                    else
                                    {
                                        _bwxQflags |= ControlFlags.PartialFailure;
                                    }
                                }
                            }
                        }
                    }
                    if(_bwxQflags == ControlFlags.None || _bwxQflags == ControlFlags.PartialFailure)
                    {
                        yield return Throw(bwxTask.Process, bwxTask.Message);
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public IEnumerator<TaskStatus> TravelByTaxi_1Proc(MentalTask bwxTask, Element destination, Element origin, float distance)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                _bwxResult = new TaskResult(bwxTask);
                _bwxMsg = new Message(MessageKind.Attempt, bwxTask, _bwxResult, new Clause(Ent_Perform, Ent_Self, Ent_callTaxi, (Element)(origin)));
                Propose(bwxTask.Process, _bwxMsg, null);
                yield return TaskStatus.Suspended;
                if(_bwxResult.Status != TaskStatus.Succeeded)
                    yield return Fail(bwxTask.Process, bwxTask.Message);
                _bwxResult = new TaskResult(bwxTask);
                _bwxMsg = new Message(MessageKind.Attempt, bwxTask, _bwxResult, new Clause(Ent_Perform, Ent_Self, Ent_ride, (Element)(null)));
                _bwxMsg.Clause[Ent_from] = origin;
                _bwxMsg.Clause[Ent_to] = destination;
                Propose(bwxTask.Process, _bwxMsg, null);
                yield return TaskStatus.Suspended;
                if(_bwxResult.Status != TaskStatus.Succeeded)
                    yield return Fail(bwxTask.Process, bwxTask.Message);
                _bwxResult = new TaskResult(bwxTask);
                _bwxMsg = new Message(MessageKind.Attempt, bwxTask, _bwxResult, new Clause(Ent_Perform, Ent_Self, Ent_payDriver, (Element)(null)));
                _bwxMsg.Clause[Ent_from] = origin;
                _bwxMsg.Clause[Ent_to] = destination;
                Propose(bwxTask.Process, _bwxMsg, null);
                yield return TaskStatus.Suspended;
                if(_bwxResult.Status != TaskStatus.Succeeded)
                    yield return Fail(bwxTask.Process, bwxTask.Message);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void CallTaxi(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element origin = (Element)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).CallTaxiProc(bwxTask, origin));
            }
            public IEnumerator<TaskStatus> CallTaxiProc(MentalTask bwxTask, Element origin)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                _bwxMsg = new Message(MessageKind.Add, bwxTask, null, new Clause(Ent_Belief, Ent_Taxi1, Ent_location, (Element)(origin)));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void Ride(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element origin = (Element)bwxMsg.Clause[Ent_from];
                Element destination = (Element)bwxMsg.Clause[Ent_to];
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).RideProc(bwxTask, origin, destination));
            }
            public IEnumerator<TaskStatus> RideProc(MentalTask bwxTask, Element origin, Element destination)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                {
                    if(bwxTask.Context.Exists(Ent_Taxi1, Ent_location, origin))
                    {
                        if(bwxTask.Context.Exists(Ent_Self, Ent_location, origin))
                        {
                            _bwxMsg = new Message(MessageKind.Modify, bwxTask, null, new Clause(Ent_Belief, Ent_Taxi1, Ent_location, (Element)(destination)));
                            Post(bwxTask.Process, _bwxMsg);
                            _bwxMsg = new Message(MessageKind.Modify, bwxTask, null, new Clause(Ent_Belief, Ent_Self, Ent_location, (Element)(destination)));
                            Post(bwxTask.Process, _bwxMsg);
                        }
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void PayDriver(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element origin = (Element)bwxMsg.Clause[Ent_from];
                Element destination = (Element)bwxMsg.Clause[Ent_to];
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).PayDriverProc(bwxTask, origin, destination));
            }
            public IEnumerator<TaskStatus> PayDriverProc(MentalTask bwxTask, Element origin, Element destination)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                {
                    _bwxClause = bwxTask.Context.Find(new ClausePattern(origin, Ent_distanceTo, destination));
                    if (_bwxClause != null)
                    {
                        float distance = (float)_bwxClause[Ent_distance];
                        {
                            foreach(float cash in bwxTask.Context.QuerySubjPred<float>(Ent_Self, Ent_cash))
                            {
                                if(cash >= 1.5 + .5 * distance)
                                {
                                    _bwxMsg = new Message(MessageKind.Modify, bwxTask, null, new Clause(Ent_Belief, Ent_Self, Ent_cash, (float)( cash - (1.5 + .5 * distance) )));
                                    Post(bwxTask.Process, _bwxMsg);
                                }
                            }
                        }
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void Impasse(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).ImpasseProc(bwxTask));
            }
            public IEnumerator<TaskStatus> ImpasseProc(MentalTask bwxTask)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                {
                    ControlFlags _bwxQflags = ControlFlags.None;
                    {
                        foreach(Element g in bwxTask.Context.QueryType(Ent_Goal))
                        {
                            if(bwxTask.Context.Exists(g, Ent_status, Ent_Active))
                            {
                                _bwxQflags |= ControlFlags.PartialSuccess;
                                _bwxMsg = new Message(MessageKind.Attempt, bwxTask, null, (Clause)g);
                                Propose(bwxTask.Process, _bwxMsg, null);
                            }
                            else
                            {
                                _bwxQflags |= ControlFlags.PartialFailure;
                            }
                        }
                    }
                    if(_bwxQflags == ControlFlags.None || _bwxQflags == ControlFlags.PartialFailure)
                    {
                        bwxTask.Process.Halt();
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void GoalElab(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element g = bwxMsg.Clause;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).GoalElabProc(bwxTask, g));
            }
            public IEnumerator<TaskStatus> GoalElabProc(MentalTask bwxTask, Element g)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                _bwxMsg = new Message(MessageKind.Add, bwxTask, null, new Clause(Ent_Belief, g, Ent_status, (Entity)(Ent_Active)));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void NotGoalElab(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Element g = bwxMsg.Clause;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((Travel)bwxExpert).NotGoalElabProc(bwxTask, g));
            }
            public IEnumerator<TaskStatus> NotGoalElabProc(MentalTask bwxTask, Element g)
            {
                Task _bwxTask;
                Context _bwxContext;
                Message _bwxMsg;
                Clause _bwxClause;
                Element _bwxSubject;
                TaskResult _bwxResult;
                _bwxMsg = new Message(MessageKind.Remove, bwxTask, null, new Clause(Ent_Belief, g, Ent_status, (Entity)(Ent_Active)));
                Post(bwxTask.Process, _bwxMsg);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
        }
        public static ElementType Ent_Element = new ElementType("Element", null);
        public static ElementType Ent_Entity = Builtins.Ent_Entity;
        public static ElementType Ent_Clause = Builtins.Ent_Clause;
        public static ElementType Ent_Belief = Builtins.Ent_Belief;
        public static ElementType Ent_Goal = Builtins.Ent_Goal;
        public static ElementType Ent_Perform = Builtins.Ent_Perform;
        public static ElementType Ent_Achieve = Builtins.Ent_Achieve;
        public static ElementType Ent_Query = Builtins.Ent_Query;
        public static ElementType Ent_Maintain = Builtins.Ent_Maintain;
        public static Entity Ent_callback = Builtins.Ent_callback;
        public static Entity Ent_status = Builtins.Ent_status;
        public static Entity Ent_Self = new Entity("Self", Ent_Entity);
        public static Entity Ent_NIL = new Entity("NIL", Ent_Entity);
        public static Entity Ent_distance = new Entity("distance", Ent_Entity);
        public static Entity Ent_cash = new Entity("cash", Ent_Entity);
        public static Entity Ent_TravelContext = new Entity("TravelContext", Ent_Entity);
        public static ElementType Ent_Taxi = new ElementType("Taxi", Ent_Entity);
        public static Entity Ent_Taxi1 = new Entity("Taxi1", Ent_Taxi);
        public static ElementType Ent_Place = new ElementType("Place", Ent_Entity);
        public static Entity Ent_distanceTo = new Entity("distanceTo", Ent_Entity);
        public static Entity Ent_Restaurant1 = new Entity("Restaurant1", Ent_Place);
        public static Entity Ent_Home1 = new Entity("Home1", Ent_Place);
        public static Entity Ent_location = new Entity("location", Ent_Entity);
        public static Entity Ent_travelTo = new Entity("travelTo", Ent_Entity);
        public static Entity Ent_travel = new Entity("travel", Ent_Entity);
        public static Entity Ent_walkTo = new Entity("walkTo", Ent_Entity);
        public static Entity Ent_callTaxi = new Entity("callTaxi", Ent_Entity);
        public static Entity Ent_ride = new Entity("ride", Ent_Entity);
        public static Entity Ent_from = new Entity("from", Ent_Entity);
        public static Entity Ent_to = new Entity("to", Ent_Entity);
        public static Entity Ent_payDriver = new Entity("payDriver", Ent_Entity);
        public static Entity Ent_Active = new Entity("Active", Ent_Entity);
    }
}
