using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class Name : Expression
    {
        public Definition Value { get; private set; }
        public bool IsVarName { get { return Token.IsVariable; } }
        public bool IsOpName { get { return Token.IsOperator; } }
        public bool IsEntityName { get { return !IsVarName && !IsOpName; } }
        public bool IsPredicateName { get { return Token.IsPredicate; } }
        public bool IsPropertyName { get { return Token.IsProperty; } }
        //
        public Name(Token token)
            : base(AstNodeKind.Name, token)
        {
        }
        public Name(Definition value)
            : base(AstNodeKind.Name, value.Token)
        {
            Value = value;
        }
        public override void CollectVariables(List<Var> vars)
        {
            if (IsVarName)
            {
                vars.Add(new Var(Token));
            }
        }

        //
        public override void Resolve() {
            base.Resolve();
            Definition value = null;
            /*if (IsVarName)
                throw new Exception();
            else*/
            if(Token.IsType)
                value = RootBlock.I.InternAtomTypeDef(Token);
            else if (IsPredicateName || IsPropertyName)
                value = RootBlock.I.InternPredicateDef(Token);
            else if(IsEntityName)
                value = RootBlock.I.InternEntityDef(Token, AtomTypeExpr);
            /*else
                throw new Exception();*/
            Value = value;
        }
        public override PredicateDef ToPredicate()
        {
            return (PredicateDef)Value;
        }
    }
}
