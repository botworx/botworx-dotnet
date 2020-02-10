using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy
{
    public class ContextDefVisit<T, N> : TopicDefVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : ContextDef
    {
        public override void DoVisit(N n)
        {
            t.WriteLine("_bwxContext = new Context();");
            string entityName = "Ent_" + n.Token.Translate();
            t.WriteLine("{0} = new Entity(\"{1}\", _bwxContext);", entityName, n.Token);
            base.DoVisit(n);
        }
    }

    public class TopicDefVisit<T, N> : StmtVisit<T, N>
        where T : Transpiler, INodeVisitor
        where N : TopicDef
    {
        public override void BeginVisit(N n)
        {
            t.PushContextPolicy();
            base.BeginVisit(n);
        }
        public override void EndVisit(N n)
        {
            base.EndVisit(n);
            t.PopPolicy();
        }
    }
}
