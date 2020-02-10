using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

using Botworx.Mia.Compile.Parse;
using Botworx.Mia.Compile.Transpile;

namespace Botworx.Mia.Compile
{
    public class Compiler
    {
        public void Compile(string inFile)
        {
            string moduleName = Path.GetFileNameWithoutExtension(inFile);
            string outFile = Path.ChangeExtension(inFile, ".cs");
            //
            Parser parser = new Parser();
            FileInfo inInfo = new FileInfo(inFile);
            FileInfo outInfo = new FileInfo(outFile);
            bool needsCompile = !outInfo.Exists || outInfo.LastWriteTime < inInfo.LastWriteTime;
            //TODO:Remove when done testing.
            needsCompile = true;
            //
            if (needsCompile)
            {
                FileStream inStream = new FileStream(inFile, FileMode.Open);
                FileStream outStream = new FileStream(outFile, FileMode.Create);
                //
                try
                {
                    parser.ParseFile(moduleName, inStream, new Transpiler(outStream));
                }
                /*catch (Exception e)
                {
                    Debug.WriteLine(e);
                    //FinishLogging();
                    throw;
                }*/
                finally
                {
                }
                //TODO:The following is for compiling and executing in memory.  Put it somewhere!
                //Compiler.Execute(path + "SequenceTest.cs", Compiler.SourceKind.File, "SequenceTestNamespace.SequenceTestModule", "MainSpawn", new object[1]);
            }
        }
    }
}
