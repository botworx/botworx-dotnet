using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Diagnostics;

using Botworx.Mia.Runtime;
using BwBrainTest;

namespace Botworx.AgentLib.ServerLib.AgentServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Agency.Instance.RegisterAssembly("BwMiaSamples");
            //RunTest("BloxBrain");
            //RunTest("SelectTestBrain");
            RunServer();
        }
        static void RunTest(string name)
        {
            Agent agent = Agency.Instance.CreateAgent(name);
            Brain brain = agent.Brain;
            brain.Run();
            Archiver.SerializeToXML(brain);
            Console.WriteLine();
            Console.WriteLine("Test finished ... Press <Enter> key to exit");
            Console.ReadLine();
        }
        static void RunServer()
        {
            AgencyServer.Instance.Run();
        }
        /*[Test]
        public static void TestSequence()
        {
            Brain brain = new SequenceTestBrain();
            brain.Run();
            Archiver.SerializeToXML(brain);
            Assert.IsTrue(true);
        }*/
    }
}
