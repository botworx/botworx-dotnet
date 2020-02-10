using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public struct ClausePattern
    {
        public AtomFlag Flags;
        public AtomType Type;
        //
        public Atom Subject;
        public Atom Predicate;
        public object Object;
        //
        public MatchFlag MatchFlags;
        //
        //TODO:probably not wise.
        public ClausePattern(Atom subj, Atom pred, object val)
        {
            Type = Expert.Ent_Belief;
            Flags = Type.MemberFlags;
            //
            Subject = subj;
            Predicate = pred;
            Object = val;
            //
            MatchFlags = MatchFlag.None;
        }
        public ClausePattern(AtomType type, Atom subj, Atom pred, object val, MatchFlag matchFlags)
        {
            Type = type;
            Flags = type.MemberFlags;
            //
            Subject = subj;
            Predicate = pred;
            Object = val;
            //
            MatchFlags = matchFlags;
        }
        //
        public bool MatchSubjPred<T>(Clause clause, out T obj)
        {
            if (Predicate == clause.AtomType && Subject == clause.Subject)
            {
                obj = (T)Object;
                return true;
            }
            //else
            obj = default(T);
            return false;
        }
        public bool MatchPredObj(Clause clause, out Atom subj)
        {
            if (Predicate == clause.AtomType && Object.Equals(clause.Object))
            {
                subj = Subject;
                return true;
            }
            //else
            subj = null;
            return false;
        }
        public bool Match(Clause clause)
        {
            MatchFlag flags = MatchFlags;
            if (!clause.TypeCheck(Type))
                return false;
            if (((flags & MatchFlag.PredicateX) != MatchFlag.PredicateX) && Predicate != clause.Predicate)
                return false;
            if (((flags & MatchFlag.SubjectX) != MatchFlag.SubjectX) && Subject != clause.Subject)
                return false;
            if ((flags & MatchFlag.ObjectX) != MatchFlag.ObjectX)
            {
                if (Object == null && clause.Object == null)
                    return true;
                if (Object == null && clause.Object != null)
                    return false;
                if (Object != null && clause.Object == null)
                    return false;
                if (!Object.Equals(clause.Object))
                    return false;
            }
            return true;
        }
    }
}
