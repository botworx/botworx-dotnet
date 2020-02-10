using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Botworx.Mia;
using Botworx.Mia.Runtime;

namespace BwBrainTest
{
    [Brain("RuleTestBrain")]
    public class RuleTestBrain : Brain
    {
        public RuleTestBrain()
        {
            Boot = Create;
            AddTrigger(new Trigger(RuleTestBrain.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_ruleTestBrain, null, MatchFlag.None))));
            AddTrigger(new Trigger(RuleTest.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_ruleTest, null, MatchFlag.None))));
        }
        public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
        {
            RuleTestBrain _bwxExpert = new RuleTestBrain();
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
            Ent_RuleTestContext = new Entity("RuleTestContext", _bwxContext);
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Bob, Ent_knows, Ent_Jack));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Bob, Ent_knows, Ent_John));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Bob, Ent_knows, Ent_Joe));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Joe, Ent_plays, Ent_Guitar));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Joe, Ent_likes, Ent_RockMusic));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Jack, Ent_plays, Ent_Piano));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_Jack, Ent_likes, Ent_ClassicalMusic));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_John, Ent_plays, Ent_Guitar));
            _bwxContext.Add(new Clause(Ent_Belief, Ent_John, Ent_likes, Ent_RockMusic));
            _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_ruleTest, null){ { Ent_context, Ent_RuleTestContext } });
            Post(bwxTask.Process, _bwxMsg);
            yield return Succeed(bwxTask.Process, bwxTask.Message);
        }
        public class RuleTest : Expert
        {
            public RuleTest()
            {
                Boot = Create;
                AddTrigger(new Trigger(RuleTest.Foo, new MessagePattern(MessageKind.Add, new ClausePattern(Ent_Belief, null, Ent_plays, Ent_Guitar, MatchFlag.SubjectX))));
                AddTrigger(new Trigger(RuleTest.BobLikes, new MessagePattern(MessageKind.Add, new ClausePattern(Ent_Belief, Ent_Bob, Ent_likes, null, MatchFlag.ObjectX))));
            }
            public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                RuleTest _bwxExpert = new RuleTest();
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
            public static void Foo(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom y = bwxMsg.Clause.Subject;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((RuleTest)bwxExpert).FooProc(bwxTask, y));
            }
            public IEnumerator<TaskStatus> FooProc(MentalTask bwxTask, Atom y)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                {
                    if(bwxTask.Context.Exists(y, Ent_likes, Ent_RockMusic))
                    {
                        if(bwxTask.Context.Exists(Ent_Bob, Ent_knows, y))
                        {
                            {
                                _bwxMsg = new Message(MessageKind.Add, null, null, new Clause(Ent_Belief, Ent_Bob, Ent_likes, y));
                                Post(bwxTask.Process, _bwxMsg);
                            }
                        }
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void BobLikes(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                Atom x = (Atom)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((RuleTest)bwxExpert).BobLikesProc(bwxTask, x));
            }
            public IEnumerator<TaskStatus> BobLikesProc(MentalTask bwxTask, Atom x)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                Console.WriteLine("Bob likes " + x.ToString());
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
        }
        public static Entity Ent_RuleTestContext = new Entity("RuleTestContext", Ent_Entity);
        public static Entity Ent_Bob = new Entity("Bob");
        public static AtomType Ent_knows = new AtomType("knows", Ent_Entity);
        public static Entity Ent_Jack = new Entity("Jack");
        public static Entity Ent_John = new Entity("John");
        public static Entity Ent_Joe = new Entity("Joe");
        public static AtomType Ent_plays = new AtomType("plays", Ent_Entity);
        public static Entity Ent_Guitar = new Entity("Guitar");
        public static AtomType Ent_likes = new AtomType("likes", Ent_Entity);
        public static Entity Ent_RockMusic = new Entity("RockMusic");
        public static Entity Ent_Piano = new Entity("Piano");
        public static Entity Ent_ClassicalMusic = new Entity("ClassicalMusic");
        public static AtomType Ent_ruleTest = new AtomType("ruleTest", Ent_Entity);
        public static AtomType Ent_ruleTestBrain = new AtomType("ruleTestBrain", Ent_Entity);
    }
}
