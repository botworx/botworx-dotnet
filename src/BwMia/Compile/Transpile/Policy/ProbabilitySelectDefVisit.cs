using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    //
    public class ProbabilitySelectDefVisit<T, N> : BlockStmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AstNode
    {
        public override void BeginVisit(N n)
        {
            base.BeginVisit(n);
            t.WriteLine("_bwxTask = new ProbabilitySelect(bwxTask);");
        }
        public override void DoVisit(N n)
        {
            float total = 0;
            int index = 0;
            float[] weights = new float[n.Children.Count];
            foreach (AstNode def in n.Children)
            {
                total += weights[index++] = float.Parse(def.Name);
            }
            index = 0;
            foreach (float w in weights)
            {
                if (index == 0)
                    weights[index] = w / total;
                else
                    weights[index] = (w / total) + weights[index - 1];
                ++index;
            }
            //
            index = 0;
            string prefix = "";
            foreach (AstNode def in n.Children)
            {
                if (index > 0)
                    prefix = "else ";
                t.WriteLine("{0}if(_bwxWeight < {1})", prefix, weights[index]);
                t.StartBlock(n);
                t.Visit(def);
                t.EndBlock(n);
                ++index;
            }
        }
    }
    public class ProbabilityCaseDefVisit<T, N> : NodeVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : AstNode
    {
        public override void BeginVisit(N n)
        {
            base.BeginVisit(n);
            t.WriteLine("_bwxTask = new Case(_bwxTask);");
        }
    }
}
