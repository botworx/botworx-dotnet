using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Irony.Ast;
using System.Collections;

namespace Botworx.Mia.Compile.Ast
{
    public delegate void AstNodeAction(AstNode node);

    public class AstNode : IBrowsableAstNode
    {
        //IBrowsableAstNode
        public int Position { get { return 0; } }
        public IEnumerable GetChildNodes() { return Children; }
        //
        public int Key { get { return (int)NodeKind; } }
        public AstNodeKind NodeKind { get; set; }
        public Token Token { get; set; }
        public string Name { get { return Token.ToString(); } }
        //
        public AstNode Parent { get; set; }
        public List<AstNode> _Children = new List<AstNode>();
        public List<AstNode> Children { get { return _Children; } }
        //TODO:Deprecate and use Slots.
        private MessageTag _MessageTag;
        public List<NodeProperty> Properties = new List<NodeProperty>();
        //
        public bool NotNil { get { return !IsNil; } }
        public bool IsNil { get { return NodeKind == AstNodeKind.Nil; } }
        public bool NotEmpty { get { return !IsEmpty; } }
        public bool IsEmpty { get { return Children.Count == 0; } }
        //
        public Atomicity Atomicity { get; set; }
        public bool IsWeaklyAtomic { get { return Atomicity == Atomicity.WeaklyAtomic; } }
        public bool IsStronglyAtomic { get{ return Atomicity == Atomicity.StronglyAtomic;}}
        public bool IsWeaklyNonAtomic { get { return Atomicity == Atomicity.WeaklyNonAtomic; } }
        public bool IsStronglyNonAtomic { get { return Atomicity == Atomicity.StronglyNonAtomic; } }
        public bool IsAtomic { get { return IsWeaklyAtomic || IsStronglyAtomic; } }
        public string LeafLabel { get; set; }
        //
        public bool IsVariable { get { if (Token == null) return false; return Token.IsVariable; } }
        public bool IsPattern { get { return NodeKind == AstNodeKind.MessagePattern; } }
        public bool IsSnippet { get { return NodeKind == AstNodeKind.Snippet; } }
        public bool IsName { get { return NodeKind == AstNodeKind.Name; } }
        //
        public AstNode(AstNodeKind kind, Token token = null)
        {
            NodeKind = kind;
            Token = token;
            Atomicity = Atomicity.WeaklyAtomic;
        }
        public void AddChild(AstNode node)
        {
            if (node == null)
                throw new Exception();
            Children.Add(node);
            node.Parent = this;

            if (!IsAtomic && node.IsWeaklyAtomic)
                node.Atomicity = Atomicity.WeaklyNonAtomic;
                
            node.LeafLabel = LeafLabel + "_" + Children.Count;
        }
        //
        public virtual void Resolve() {
            foreach (var child in Children)
            {
                child.Resolve();
            }
        }
        public virtual void AddTriggerDef(TriggerStmt def, bool isCreator) {}
        //
        public MessageTag MessageTag
        {
            get
            {
                if (_MessageTag == null)
                    _MessageTag = new MessageTag(this);
                return _MessageTag;
            }
        }
        //
        public void AddProperty(NodeProperty property)
        {
            OnAddProperty(property);
            Properties.Add(property);
        }
        public virtual void OnAddProperty(NodeProperty property) {}
        public bool HasProperties
        {
            get { return Properties.Count != 0; }
        }
        //
        public virtual bool IsExitable { get { return false; } }
        public virtual bool NeedsExit
        {
            get { return false; }
            set 
            {
                Parent.NeedsExit = value;
            }
        }
        public bool NeedsInnerExit = false;
        public string GenerateLabel(string prefix)
        {
            return prefix + LeafLabel + ":";
        }
        public string GenerateGoto(string prefix)
        {
            return "goto " + prefix + LeafLabel + ";" ;
        }
        public string GenerateExitLabel()
        {
            return GenerateLabel("exit");
        }
        public string GenerateExitGoto()
        {
            return GenerateGoto("exit");
        }
        public string GenerateInnerExitGoto()
        {
            return GenerateGoto("innerexit");
        }
        public string GenerateInnerExitLabel()
        {
            return GenerateLabel("innerexit");
        }
        //
        public static string FindPath(AstNode a, AstNode b)
        {
            //TODO:Just doing inter-namespace for now.
            string path = b.Name;
            AstNode def = b.Parent;
            while (def != null)
            {
                if (def is RootBlock || def == a)
                    break;
                path = def.Name + "." + path;
                def = def.Parent;
            }
            return path;
        }
        //
        public string Line
        {
            get
            {
                if (Token != null)
                    return Token.Line;
                else
                    return null;
            }
        }
        public override string ToString()
        {
            if (Token != null)
                return base.ToString() + " : " + Token.ToString();
            else
                return base.ToString();
        }
    }
    public class NodeProperty
    {
        public Token Predicate;
        public Token Object;
    }
}
