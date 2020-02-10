using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Botworx.Mia.Compile.Ast;

namespace Botworx.Mia.Compile.Transpile.Policy.Lhs
{
    public class LhsClauseExprVisit<N> : ClauseExprVisit<N>
        where N : ClauseExpr
    {
        protected delegate bool Delegate(Transpiler t, ClauseExpr d);
        [Flags]
        protected enum Flags : int
        {
            Default = 0,
            FS = 1, //Free Subject
            TS = 2, //Typed Subject
            FP = 4, //Free Predicate
            TP = 8, //Typed Predicate
            FO = 16, //Free Object
            TO = 32, //Typed Object
            U = 64, //Unary Operator
            B = 128 //Binary Operator
        }
        protected static Dictionary<Flags, Delegate> Delegates = new Dictionary<Flags, Delegate>();
        //
        static LhsClauseExprVisit()
        {
            AddDelegate(Flags.Default, Write_Default);
            AddDelegate(Flags.U, Write_U);
            AddDelegate(Flags.B, Write_B);
            //
            AddDelegate(Flags.TS, Write_TS);
            //
            AddDelegate(Flags.FS, Write_FS);
            AddDelegate(Flags.FS | Flags.TS, Write_FS_TS);
            AddDelegate(Flags.FS | Flags.TS | Flags.U, Write_FS_TS_U);
            AddDelegate(Flags.FO, Write_FO);
        }
        //
        protected static void AddDelegate(Flags f, Delegate d)
        {
            Delegate overrode;
            if (Delegates.TryGetValue(f, out overrode))
                Delegates.Remove(f);
            Delegates.Add(f, d);
        }
        //
        static bool Write_Default(Transpiler t, ClauseExpr d)
        {
            if (d.IsProperty)
            {
                t.WriteLine("if (_bwxClause != null && _bwxClause.Exists<{0}>({1}, {2}))",
                    d.Predicate.ToPredicate().Spec, t.Translate(d.Predicate), t.Translate(d.Object));
            }
            /*else if (d.HasMetaDefs)
            {
                t.WriteLine("_bwxClause = bwxTask.Context.Find(new ClausePattern({0}, {1}, {2}));",
                    t.Translate(d.Subject), t.Translate(d.Predicate), t.Translate(d.Object));
                t.WriteLine("if (_bwxClause != null)");
            }*/
            else
            {
                if (d.Object.IsSnippet)
                {
                    t.WriteLine("if(bwxTask.Context.Exists<{0}>({1}, {2}, {3}))",
                        d.Type, t.Translate(d.Subject), t.Translate(d.Predicate), t.Translate(d.Object));
                }
                else
                {
                    t.WriteLine("if(bwxTask.Context.Exists({0}, {1}, {2}))",
                        t.Translate(d.Subject), t.Translate(d.Predicate), t.Translate(d.Object));
                }
            }
            return false;
        }
        static bool Write_U(Transpiler t, ClauseExpr d)
        {
            t.WriteLine("if(!bwxTask.Context.Exists({0}, {1}, {2}))",
                t.Translate(d.Subject), t.Translate(d.Predicate), t.Translate(d.Object));
            return false;
        }
        static bool Write_B(Transpiler t, ClauseExpr d)
        {
            t.WriteLine("if({0} {1} {2})",
                t.Translate(d.Subject), t.Translate(d.Predicate), t.Translate(d.Object));
            return false;
        }
        //
        static bool Write_TS(Transpiler t, ClauseExpr d)
        {
            t.WriteLine("if({0}.TypeCheck({1}) && bwxTask.Context.Exists({0}, {2}, {3}))",
                t.Translate(d.Subject), t.Translate(d.Subject.AtomTypeExpr), t.Translate(d.Predicate), t.Translate(d.Object));
            return false;
        }
        //
        static bool Write_FS(Transpiler t, ClauseExpr d)
        {
            t.WriteLine("foreach({0} {1} in bwxTask.Context.QueryPredObj({2}, {3}))",
                            "Atom", t.Translate(d.Subject), t.Translate(d.Predicate), t.Translate(d.Object));
            return true;
        }
        static bool Write_FS_TS(Transpiler t, ClauseExpr d)
        {
            t.WriteLine("foreach({0} {1} in bwxTask.Context.QueryType({2}))",
                            d.Subject.Type, t.Translate(d.Subject), t.Translate(d.Subject.AtomTypeExpr));
            t.StartBlock(null);
            Write_Default(t, d);
            return false;
        }
        static bool Write_FS_TS_U(Transpiler t, ClauseExpr d)
        {
            t.WriteLine("foreach({0} {1} in bwxTask.Context.QueryType({2}))",
                            d.Subject.Type, t.Translate(d.Subject), t.Translate(d.Subject.AtomTypeExpr));
            t.StartBlock(null);
            Write_U(t, d);
            return false;
        }
        static bool Write_FO(Transpiler t, ClauseExpr d)
        {
            if (d.IsProperty)
            {
                t.WriteLine("{0} {1} = ({0})_bwxClause[{2}];",
                    d.Type, t.Translate(d.Object), t.Translate(d.Predicate));
                return false;
            }
            t.WriteLine("foreach({0} {1} in bwxTask.Context.QuerySubjPred<{0}>({2}, {3}))",
                //d.Type, t.Translate(d.Object), t.Translate(d.Subject), t.Translate(d.Predicate));
                //d.Predicate.ToPredicate().Spec, t.Translate(d.Object), t.Translate(d.Subject), t.Translate(d.Predicate));
                d.Object.Type, t.Translate(d.Object), t.Translate(d.Subject), t.Translate(d.Predicate));
            return true;
        }
        //
        public override void DoVisit(N n)
        {
            Token subj = n.Subject.Token;
            Token pred = n.Predicate.Token;
            Token obj = n.Object.Token;
            //
            Var subjVar, predVar, objVar;
            bool unknownSubj = t.InternVariable(subj, out subjVar);
            bool unknownPred = t.InternVariable(pred, out predVar);
            bool unknownObj = t.InternVariable(obj, n.Predicate.Token, out objVar);
            //
            Flags flags = Flags.Default;
            if (unknownSubj)
                flags |= Flags.FS;
            if (unknownPred)
                flags |= Flags.FP;
            if (unknownObj)
                flags |= Flags.FO;
            if (n.Negated)
                flags |= Flags.U;
            if (n.IsBinary)
                flags |= Flags.B;
            if (n.HasSubjectTypeConstraint)
                flags |= Flags.TS;
            if (n.HasObjectTypeConstraint)
                flags |= Flags.TO;
            //
            Delegate d = null;
            Delegates.TryGetValue(flags, out d);
            if (d != null)
            {
                t.CurrentStmt.IsIterator = d(t, n);
            }
            else
                t.WriteLine("!!!:ClauseCondition:Implement: Match:  " + flags);
            //
            //base.DoVisit(n);
        }
    }
}
