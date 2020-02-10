using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Context
    {
        public static Context Global = new Context();
        //
        Dictionary<string, Atom> Dictionary = new Dictionary<string, Atom>();
        public List<Clause> Clauses = new List<Clause>();
        //
        public Atom Intern(string name)
        {
            Atom value;
            if (!Dictionary.TryGetValue(name, out value))
            {
                value = new Entity(name);
                Dictionary.Add(name, value);
            }
            return value;
        }
        //
        public Atom Add(Clause clause)
        {
            if (!Clauses.Exists(x => x.Equals(clause)))
                Clauses.Add(clause);
            return clause;
        }
        public void Remove(Clause clause)
        {
            Clauses.Remove(clause);
        }
        public Clause Modify(Clause clause)
        {
            Clause replacedClause = null;
            foreach (var c in Clauses)
            {
                if (c.AtomType != clause.AtomType)
                    continue;
                if (c.Subject != clause.Subject)
                    continue;
                replacedClause = c;
                Remove(c);
                Add(clause);
                break;
            }
            return replacedClause;
        }
        public Atom Find(ClausePattern pattern)
        {
            foreach (var clause in Clauses)
            {
                if (pattern.Match(clause))
                    return clause;
            }
            return null;
        }
        public IEnumerable<Atom> QueryType(AtomType type)
        {
            foreach (var clause in Clauses)
            {
                if (clause.TypeCheck(type))
                    yield return clause;
            }
        }
        public IEnumerable<T> QuerySubjPred<T>(Atom subj, AtomType pred)
        {
            T obj;
            foreach (var clause in Clauses)
            {
                if (clause.Predicate != pred)
                    continue;
                if (!clause.MatchSubjPred(subj, pred, out obj))
                    continue;
                //else
                yield return obj;
            }
        }
        public IEnumerable<Atom> QueryPredObj<T>(AtomType pred, T obj)
        {
            Atom subj;
            foreach (var clause in Clauses)
            {
                if (clause.Predicate != pred)
                    continue;
                if (!clause.MatchPredObj(pred, obj, out subj))
                    continue;
                //else
                yield return subj;
            }
        }
        public bool Exists<T>(Atom subj, AtomType pred, T obj)
        {
            ClausePattern target = new ClausePattern(subj, pred, obj);
            foreach (var clause in Clauses)
            {
                if (clause.Predicate != pred)
                    continue;
                if (target.Match(clause))
                    return true;
            }
            return false;
        }
        public bool Exists<T>(Atom subj, Atom pred, Predicate<T> predicate)
        {
            foreach (var clause in Clauses)
            {
                if (clause.AtomType != pred)
                    continue;
                if (clause.Subject != subj)
                    continue;
                return predicate((T)clause.Object);
            }
            //else
            return false;
        }
    }
}
