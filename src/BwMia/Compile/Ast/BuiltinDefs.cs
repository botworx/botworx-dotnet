using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public static class BuiltinDefs
    {
        //Types
        public static AtomTypeDef Atom = new AtomTypeDef(TokenInstance.CSharp.TYPE_ATOM, null, AtomFlag.Atom);
        public static AtomTypeDef Entity = new AtomTypeDef(TokenInstance.CSharp.TYPE_ENTITY, Atom, AtomFlag.Entity);
        public static AtomTypeDef Clause = new AtomTypeDef(TokenInstance.CSharp.TYPE_CLAUSE, Atom, AtomFlag.Clause);
        public static AtomTypeDef Belief = new AtomTypeDef(TokenInstance.CSharp.TYPE_BELIEF, Clause, AtomFlag.Belief);
        public static AtomTypeDef Goal = new AtomTypeDef(TokenInstance.CSharp.TYPE_GOAL, Clause, AtomFlag.Goal);
        public static AtomTypeDef Perform = new AtomTypeDef(TokenInstance.CSharp.TYPE_PERFORM, Goal, AtomFlag.Perform);
        public static AtomTypeDef Achieve = new AtomTypeDef(TokenInstance.CSharp.TYPE_ACHIEVE, Goal, AtomFlag.Achieve);
        public static AtomTypeDef Query = new AtomTypeDef(TokenInstance.CSharp.TYPE_QUERY, Goal, AtomFlag.Query);
        public static AtomTypeDef Maintain = new AtomTypeDef(TokenInstance.CSharp.TYPE_MAINTAIN, Goal, AtomFlag.Maintain);
        //Entities
        public static EntityDef Blank = new EntityDef(TokenInstance.CSharp.BLANK, true);
        public static EntityDef Nil = new EntityDef(TokenInstance.CSharp.NIL, true);
        public static EntityDef Self = new EntityDef(TokenInstance.CSharp.SELF, true);
        //
        public static EntityDef Active = new EntityDef(TokenInstance.CSharp.SELF, true);
        //Predicates
        public static PredicateDef Callback = new PredicateDef(TokenInstance.CSharp.PRED_CALLBACK, BuiltinDefs.Perform, new Token("MessageCallback"), true);
        //
        public static PredicateDef Status = new PredicateDef(TokenInstance.CSharp.PRED_STATUS, BuiltinDefs.Belief, TokenInstance.CSharp.ENTITY, true);
        public static PredicateDef Context = new PredicateDef(TokenInstance.CSharp.PRED_CONTEXT, BuiltinDefs.Belief, TokenInstance.CSharp.ENTITY, true);
        //Operators
        public static PredicateDef NotEqual = new PredicateDef(TokenInstance.CSharp.OP_NOTEQUAL, BuiltinDefs.Belief, TokenInstance.CSharp.ENTITY, true);
    }
}
