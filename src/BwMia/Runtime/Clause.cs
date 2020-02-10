using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Clause : Atom
    {
        public Atom Subject;
        public AtomType Predicate;
        public object Object;

        public Clause(Atom subj, AtomType pred, object val)
        {
            AtomType = Expert.Ent_Belief;
            Subject = subj;
            Predicate = pred;
            Object = val;
        }
        public Clause(AtomType type, Atom subj, AtomType pred, object val)
        {
            AtomType = type;
            Subject = subj;
            Predicate = pred;
            Object = val;
        }
        //
        public bool MatchSubjPred<T>(Atom subj, AtomType pred, out T obj)
        {
            if (Predicate == pred && Subject == subj)
            {
                obj = (T)Object;
                return true;
            }
            //else
            obj = default(T);
            return false;
        }
        public bool MatchPredObj(AtomType pred, object obj, out Atom subj)
        {
            if (Predicate == pred && Object.Equals(obj))
            {
                subj = Subject;
                return true;
            }
            //else
            subj = null;
            return false;
        }
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;
            Clause clause = obj as Clause;
            if (clause == null)
                return false;
            //else
            return Equals(clause);
        }
        public bool Equals(Clause clause)
        {
            if (clause == null)
                return false;
            //else
            if (AtomType != clause.AtomType)
                return false;
            //else
            if (Subject != clause.Subject)
                return false;
            //else
            if (Predicate != clause.Predicate)
                return false;
            //else
            if (Object == null && clause.Object == null)
                return true;
            //else
            if (Object == null && clause.Object != null)
                return false;
            //else
            if (Object != null && clause.Object == null)
                return false;
            //else
            if (!Object.Equals(clause.Object))
                return false;
            //else
            return true;
        }
        public override int GetHashCode()
        {
            return 0; //TODO: WHAT!!!!
            //return Value.GetHashCode() ^ Kind.GetHashCode();
        }
        public override string ToString()
        {
            string subjectString = Subject.ToString();
            if (Subject is Clause)
                subjectString += ", ";

            string objectString;
            if (Object != null)
                objectString = Object.ToString();
            else
                objectString = "null";

            string clauseString = string.Format("{0} {1} {2}", subjectString, Predicate.ToString(), objectString);
            if (Properties != null)
            {
                foreach (var property in Properties)
                {
                    clauseString += " :" + property.Key.ToString() + " " + property.Value.ToString();
                }
            }
            return clauseString;
        }
    }
}