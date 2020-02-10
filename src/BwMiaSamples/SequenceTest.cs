using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Botworx.Mia;
using Botworx.Mia.Runtime;

namespace BwBrainTest
{
    [Brain("SequenceTestBrain")]
    public class SequenceTestBrain : Brain
    {
        public SequenceTestBrain()
        {
            Boot = Create;
            AddTrigger(new Trigger(SequenceTestBrain.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_sequenceTestBrain, null, MatchFlag.None))));
            AddTrigger(new Trigger(SequenceTest.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_sequenceTest, null, MatchFlag.None))));
        }
        public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
        {
            SequenceTestBrain _bwxExpert = new SequenceTestBrain();
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
            _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_sequenceTest, null));
            Post(bwxTask.Process, _bwxMsg);
            yield return Succeed(bwxTask.Process, bwxTask.Message);
        }
        public class SequenceTest : Expert
        {
            public SequenceTest()
            {
                Boot = Create;
                AddTrigger(new Trigger(SequenceTest.WriteLine, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_writeLine, null, MatchFlag.ObjectX))));
            }
            public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                SequenceTest _bwxExpert = new SequenceTest();
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
                _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, Ent_a));
                Post(bwxTask.Process, _bwxMsg);
                {
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 1));
                    Post(bwxTask.Process, _bwxMsg);
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 2));
                    Post(bwxTask.Process, _bwxMsg);
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 3));
                    Post(bwxTask.Process, _bwxMsg);
                }
                _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, Ent_b));
                Post(bwxTask.Process, _bwxMsg);
                {
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 1));
                    Post(bwxTask.Process, _bwxMsg);
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 2));
                    Post(bwxTask.Process, _bwxMsg);
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 3));
                    Post(bwxTask.Process, _bwxMsg);
                }
                _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, Ent_c));
                Post(bwxTask.Process, _bwxMsg);
                {
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 1));
                    Post(bwxTask.Process, _bwxMsg);
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 2));
                    Post(bwxTask.Process, _bwxMsg);
                    _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 3));
                    Post(bwxTask.Process, _bwxMsg);
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void WriteLine(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                object text = (object)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((SequenceTest)bwxExpert).WriteLineProc(bwxTask, text));
            }
            public IEnumerator<TaskStatus> WriteLineProc(MentalTask bwxTask, object text)
            {
                Task _bwxTask = null;
                Context _bwxContext = null;
                Message _bwxMsg;
                Clause _bwxClause = null;
                Atom _bwxSubject = null;
                TaskResult _bwxResult = null;
                Begin(bwxTask);
                Console.WriteLine(text);
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
        }
        public static AtomType Ent_writeLine = new AtomType("writeLine", Ent_Entity);
        public static AtomType Ent_sequenceTest = new AtomType("sequenceTest", Ent_Entity);
        public static AtomType Ent_sequenceTestBrain = new AtomType("sequenceTestBrain", Ent_Entity);
        public static Entity Ent_a = new Entity("a");
        public static Entity Ent_b = new Entity("b");
        public static Entity Ent_c = new Entity("c");
    }
}
