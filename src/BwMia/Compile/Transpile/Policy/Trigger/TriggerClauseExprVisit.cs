using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Trigger
{
    public class TriggerClauseExprVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ClauseExpr
    {
        public override void DoVisit(N n)
        {
            t.Write("new ClausePattern({0}, ", t.Translate(n.AtomTypeExpr));
            WriteConstituents(n);
            if(n.Subject.IsVariable)
                n.MatchFlags |= MatchFlag.SubjectX;
            if (n.Predicate.IsVariable)
                n.MatchFlags |= MatchFlag.PredicateX;
            if (n.Object.IsVariable)
                n.MatchFlags |= MatchFlag.ObjectX;
            WriteMatchFlags(n);
            t.Write(")");
        }
        public void WriteMatchFlags(N n)
        {
            string flags = "";
            string[] names = Enum.GetNames(typeof(MatchFlag));
            MatchFlag flag;
            for (int i = 0; i < names.Length; ++i)
            {
                flag = (MatchFlag)(1 << i);
                if ((n.MatchFlags & flag) == flag)
                {
                    if (flags != "")
                        flags += " | ";
                    flags += "MatchFlag." + names[i + 1];
                }
            }
            if (flags == "")
                t.Write(", MatchFlag.None");
            else
                t.Write(", {0}", flags);
        }
        public void WriteConstituents(N n)
        {
            Token subjToken = n.Subject.Token;
            string subjName;
            if (n.Subject.IsVariable)
                subjName = TokenInstance.CSharp.NIL.Translate();
            else
                subjName = t.Translate(n.Subject);
            //
            Token predToken = n.Predicate.Token;
            string predName;
            if (n.Predicate.IsVariable)
                predName = TokenInstance.CSharp.NIL.Translate();
            else
                predName = t.Translate(n.Predicate);
            //
            Token objToken = n.Object.Token;
            string objName;
            if (n.Object.IsVariable)
                objName = TokenInstance.CSharp.NIL.Translate();
            else
                objName = t.Translate(n.Object);
            //
            t.Write("{0}, {1}, {2}", subjName, predName, objName);
        }
    }
}
