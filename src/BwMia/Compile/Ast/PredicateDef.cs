using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class PredicateDef : EntityDef
    {
        public AtomTypeDef ClauseType;
        public Token Spec { get; set; } //Type of the Object slot.
        public PredicateCardinality Cardinality;
        //
        public PredicateDef(Token token)
            : base(AstNodeKind.PredicateDef, token)
        {
            ClauseType = BuiltinDefs.Belief;
            Spec = TokenInstance.CSharp.ELEMENT;
        }
        public PredicateDef(Token token, AtomTypeDef clauseType, Token spec)
            : this(token, clauseType, spec, false)
        {
        }
        public PredicateDef(Token token, AtomTypeDef clauseType, Token spec, bool isBuiltin)
            : base(AstNodeKind.PredicateDef, token, isBuiltin)
        {
            ClauseType = clauseType;
            Spec = spec;
        }
    }
    public enum PredicateCardinality
    {
        OneToOne = 0,
        OneToMany,
        ManyToOne,
        ManyToMany
    }
}
