using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Botworx.Mia;
using Botworx.Mia.Runtime;

namespace BwBrainTest
{
    [Brain("SelectTestBrain")]
    public class SelectTestBrain : Brain
    {
        public SelectTestBrain()
        {
            Boot = Create;
            AddTrigger(new Trigger(SelectTestBrain.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_selectTestBrain, null, MatchFlag.None))));
            AddTrigger(new Trigger(SelectTest.Create, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_selectTest, null, MatchFlag.None))));
        }
        public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
        {
            SelectTestBrain _bwxExpert = new SelectTestBrain();
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
            _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_selectTest, null));
            Post(bwxTask.Process, _bwxMsg);
            yield return Succeed(bwxTask.Process, bwxTask.Message);
        }
        public class SelectTest : Expert
        {
            public SelectTest()
            {
                Boot = Create;
                AddTrigger(new Trigger(SelectTest.WriteLine, new MessagePattern(MessageKind.Attempt, new ClausePattern(Ent_Perform, Ent_Self, Ent_writeLine, null, MatchFlag.ObjectX))));
            }
            public static void Create(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                SelectTest _bwxExpert = new SelectTest();
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
                 ConsoleKeyInfo cki = Console.ReadKey(); 
                 var c = cki.KeyChar; 
                {
                    if( c == 'a' )
                    {
                        _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 1));
                        Post(bwxTask.Process, _bwxMsg);
                    }
                    if( c == 'b' )
                    {
                        _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 2));
                        Post(bwxTask.Process, _bwxMsg);
                    }
                    if( c == 'c' )
                    {
                        _bwxMsg = new Message(MessageKind.Attempt, null, null, new Clause(Ent_Perform, Ent_Self, Ent_writeLine, 3));
                        Post(bwxTask.Process, _bwxMsg);
                    }
                }
                yield return Succeed(bwxTask.Process, bwxTask.Message);
            }
            public static void WriteLine(Process bwxProcess, Expert bwxExpert, Message bwxMsg)
            {
                object text = (object)bwxMsg.Clause.Object;
                MentalTask bwxTask = new Method(bwxProcess, bwxMsg);
                bwxProcess.ScheduleTask(bwxTask, ((SelectTest)bwxExpert).WriteLineProc(bwxTask, text));
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
        public static AtomType Ent_selectTest = new AtomType("selectTest", Ent_Entity);
        public static AtomType Ent_selectTestBrain = new AtomType("selectTestBrain", Ent_Entity);
    }
}
