//snarfed from ziggyware.com

using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Reflection;
using Microsoft.CSharp;
using System.Security;
using System.Security.Permissions;
using System.IO;

namespace Botworx.Mia.Compile
{
    public class Assembler
    {
        public enum SourceKind
        {
            String,
            File
        }
        public static object Execute(string source, SourceKind kind, string className, string functionName, object[] parameters)
        {
            return Execute(Compile(source, kind), className, functionName, parameters);
        }

        public static object Execute(Assembly assembly,
                                     string className,
                                     string functionName,
                                     object[] parameters)
        {
            object o = null;
            return Execute(assembly, className, functionName, parameters, ref o);
        }

        public static object Execute(Assembly a,
                                     string className,
                                     string functionName,
                                     object[] parameters,
                                     ref object callingObject)
        {
            object o = null;

            if (functionName.Length > 0)
            {
                Type t = a.GetType(className);
                MethodInfo info = t.GetMethod(functionName);

                if (parameters == null) parameters = new object[0];
                if (info != null)
                {
                    if (info.IsStatic)
                    {
                        o = info.Invoke(null, parameters);
                    }
                    else
                    {
                        if (callingObject == null)
                        {
                            callingObject = t.Assembly.CreateInstance(t.FullName);
                        }
                        o = info.Invoke(callingObject, parameters);
                    }
                }
            }
            else
            {
                Type t = a.GetType(className);

                callingObject = t.Assembly.CreateInstance(t.FullName);
            }

            return o;
        }

        public static Assembly Compile(String source, SourceKind kind)
        {
            CodeDomProvider provider = new CSharpCodeProvider();

            CompilerParameters cp = new CompilerParameters();

            // Generate a class library.
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;

            // Generate debug information.
            //cp.IncludeDebugInformation = false;
            cp.IncludeDebugInformation = true;

            // Add an assembly reference.


            Assembly a = Assembly.GetExecutingAssembly();

            foreach (AssemblyName assemblyName in
                            Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                cp.ReferencedAssemblies.Add(assemblyName.Name + ".dll");
            }


            // Set the level at which the compiler 
            // should start displaying warnings.
            //cp.WarningLevel = 3;
            cp.WarningLevel = 0;

            // Set whether to treat all warnings as errors.
            cp.TreatWarningsAsErrors = false;


            // Set compiler argument to optimize output.
            cp.CompilerOptions = "/optimize";


            // Invoke compilation.
            CompilerResults cr = null;
            switch(kind){
                case SourceKind.String:
                    cr = provider.CompileAssemblyFromSource(
                                        cp, new string[] { source });
                    break;
                case SourceKind.File:
                    cr = provider.CompileAssemblyFromFile(
                                        cp, new string[] { source });
                    break;
            }

            if (cr.Errors.Count > 0)
            {
                // Display compilation errors.
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("Errors building {0} into {1}\n\n",
                                    source, cr.PathToAssembly);

                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append("\n");

                }
                throw new Exception(sb.ToString());
            }
            else
            {
                return cr.CompiledAssembly;
            }
        }
    }
}