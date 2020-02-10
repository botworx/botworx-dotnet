using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using Botworx.Mia.Compile;

namespace BwBrainTestGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Builder builder = new Builder();
            string inputDirectory = "../../../BwMiaSamples/";
            //builder.BuildDirectory(inputDirectory);
            //builder.BuildFile(inputDirectory + "Blox.bws");
            //builder.BuildFile(inputDirectory + "RuleTest.bws");
            //builder.BuildFile(inputDirectory + "BloxAchieve.bws");
            //builder.BuildFile(inputDirectory + "Counting.bws");
            //builder.BuildFile(inputDirectory + "SequenceTest.bws");
            //builder.BuildFile(inputDirectory + "SelectTest.bws");
            //
            builder.BuildFiles(inputDirectory, new[] { "Blox.bws", "RuleTest.bws", "BloxAchieve.bws", "Counting.bws", "SequenceTest.bws", "SelectTest.bws" });
            //Console.ReadLine();
        }
    }
}
