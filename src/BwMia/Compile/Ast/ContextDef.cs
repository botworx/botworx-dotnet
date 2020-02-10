using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class TopicDef : Stmt
    {
        //
        public TopicDef(ExprSeq exprSeq)
            : this(AstNodeKind.TopicDef, null)
        {
            ExprSeq = exprSeq;
        }
        public TopicDef(Token token)
            : this(AstNodeKind.TopicDef, token)
        {
        }
        public TopicDef(AstNodeKind kind, Token token)
            : base(kind, token)
        {
        }
    }

    public enum ContextDefKind
    {
        Default = 0,
        Inline,
        Next,
        Previous,
        Indexed,
        Named
    }
    public class ContextDef : TopicDef
    {
        public ContextDefKind ContextDefKind;
        public ContextDef(Token token)
            : base(AstNodeKind.ContextDef, token)
        {
        }
        public TopicDef CreateTopicDef(ExprSeq exprSeq)
        {
            TopicDef def = new TopicDef(exprSeq);
            AddChild(def);
            return def;
        }
    }
}
