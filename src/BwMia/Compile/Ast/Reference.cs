using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public interface IReference
    {
        IKlassDef KlassDef { get; set; }
    }
    public class Reference
    {
        Token RefToken;
        public Reference(Token token)
        {
            RefToken = token;
        }
    }
    public class NodeRef<T> : Reference, INode
        where T : Node
    {
        public T Target;
        public NodeRef(Token token, T target = null)
            :base(token)
        {
            Target = target;
        }
        public int Key { get { return (int)NodeKind.Reference; } }
        public NodeKind NodeKind { get { return Target.NodeKind; } }
        public Token Token { get { return Target.Token; } set { Target.Token = value; } }
        public bool IsVariable { get { return Target.IsVariable; } }
        public bool IsSnippet { get { return Target.IsSnippet; } }
        public bool IsReference { get { return Target.IsReference; } }
        public List<INode> Children { get { return Target.Children; } }
    }
    public class ObjectRef<T> : NodeRef<T>, IObjectDef
        where T : ObjectDef
    {
        public Token Spec
        {
            get { return Target.Spec; }
            set { Target.Spec = value; }
        }
        //
        public ObjectRef(Token token, T target = null)
            : base(token, target)
        {
        }
    }
    public class AtomRef<T> : ObjectRef<T>, IAtomDef
        where T : AtomDef
    {
        public IKlassDef KlassDef
        {
            get { return Target.KlassDef; }
            set { Target.KlassDef = value; }
        }        //
        //
        public AtomRef(Token token, T target = null)
            : base(token, target)
        {
        }
    }
    public class EntityRef<T> : AtomRef<T>, IEntityDef
        where T : EntityDef
    {
        //
        public EntityRef(Token token, T target = null)
            : base(token, target)
        {
        }
    }
    public class AtomTypeRef<T> : EntityRef<T>, IKlassDef
        where T : KlassDef
    {
        //
        public AtomTypeRef(Token token, T target = null)
            : base(token, target)
        {
        }
    }
    public class PredicateRef<T> : EntityRef<T>, IPredicateDef
        where T : PredicateDef
    {
        //
        public PredicateRef(Token token, T target = null)
            : base(token, target)
        {
        }
    }
    public class ReferenceVisit<T, N> : Visit<T, N>
        where T : Transpiler, IVisitor
        where N : NodeRef<Node>
    {
        public override void DoVisit(N n, INode g)
        {
            //t.Write(n.Name);
            t.Visit(n.Target, n);
        }
    }

}
