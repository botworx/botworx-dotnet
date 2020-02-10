using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class ClauseExpr : Expression
    {
        public Expression Subject { get; set; }
        public Expression Predicate { get; set; }
        public Expression Object { get; set; }
        //
        public bool IsProperty { get { return NodeKind == AstNodeKind.PropertyExpr; } }
        //
        public bool IsCallback { get { return Predicate.ToPredicate() == BuiltinDefs.Callback; } }
        //As Pattern
        public MatchFlag MatchFlags;
        //
        public ClauseExpr(Expression subject, Expression predicate, Expression object_)
            : this(AstNodeKind.ClauseExpr, null)
        {
            Subject = subject;
            Predicate = predicate;
            Object = object_;
        }
        public ClauseExpr(Expression predicate, Expression object_)
            : this(AstNodeKind.PropertyExpr, null)
        {
            Predicate = predicate;
            Object = object_;
        }
        public ClauseExpr()
            : this(AstNodeKind.ClauseExpr, null)
        {
        }
        public ClauseExpr(AstNodeKind kind, Token token)
            : base(kind, token)
        {
            AtomTypeExpr = BuiltinDefs.Clause.CreateName();
        }
        //
        public override void Resolve()
        {
            base.Resolve();
            if(Subject != null) //For property expressions. Hmmm...
                Subject.Resolve();  
            Predicate.Resolve();
            Object.Resolve();
            //
            PredicateDef predDef = Predicate.ToPredicate();
            if(predDef != null)
                Object.Type = predDef.Spec;

            if(AtomTypeExpr == null)
                AtomTypeExpr = new Name(predDef.ClauseType);

            foreach (var propExpr in PropertyExprs)
                propExpr.Resolve();
        }
        //
        public void MakeCallback()
        {
            Subject = BuiltinDefs.Self.CreateName();
            Predicate = BuiltinDefs.Callback.CreateName();
            Object = null;
        }
        public virtual bool IsBinary
        {
            get { return false; }
        }
        public bool HasSubjectTypeConstraint
        {
            get { return Subject.AtomTypeExpr != null; }
        }
        public bool HasObjectTypeConstraint
        {
            get { return Object.AtomTypeExpr != null; }
        }
        public override void CollectVariables(List<Var> vars)
        {
            if (Binding != null)
            {
                vars.Add(new Var(Binding));
            }
            if (Subject != null && Subject.IsVariable)
            {
                vars.Add(new Var(Subject.Token));
            }
            if (Object != null && Object.IsVariable)
            {
                vars.Add(new Var(Object.Token, Object.Type));
            }
            foreach (ClauseExpr propExpr in PropertyExprs)
            {
                propExpr.CollectVariables(vars);
            }
        }
        public override string ToString()
        {
            string clauseString = string.Format("{0} {1} {2}", Subject.ToString(), Predicate.ToString(), Object.ToString());
            if (PropertyExprs != null)
            {
                foreach (var property in PropertyExprs)
                {
                    clauseString += " :" + property.Predicate.ToString() + " " + property.Object.ToString();
                }
            }
            return clauseString;
        }
    }
}
