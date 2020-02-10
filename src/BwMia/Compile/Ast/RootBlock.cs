using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Compile.Ast
{
    public class RootBlock : NamespaceBlock
    {
        public static RootBlock I;
        public Dictionary<string, EntityDef> EntityDictionary = new Dictionary<string, EntityDef>();
        public BrainDef BrainDef; //The class that represents this module
        //
        public static RootBlock Create(string name)
        {
            I = new RootBlock(new Token(name));
            return I;
        }
        private RootBlock(Token token)
            : base(AstNodeKind.RootBlock, token)
        {
            AddEntityDef(BuiltinDefs.Atom);
            AddEntityDef(BuiltinDefs.Entity);
            AddEntityDef(BuiltinDefs.Clause);
            AddEntityDef(BuiltinDefs.Belief);
            AddEntityDef(BuiltinDefs.Goal);
            AddEntityDef(BuiltinDefs.Perform);
            AddEntityDef(BuiltinDefs.Achieve);
            AddEntityDef(BuiltinDefs.Query);
            AddEntityDef(BuiltinDefs.Maintain);
            //
            AddEntityDef(BuiltinDefs.Callback);
            AddEntityDef(BuiltinDefs.Status);
            AddEntityDef(BuiltinDefs.Context);
            //
            AddEntityDef(BuiltinDefs.Self);
            AddEntityDef(BuiltinDefs.Nil);
        }
        public void AddEntityDef(EntityDef entityDef)
        {
            EntityDictionary[entityDef.Token.ToString()] = entityDef;
        }
        public EntityDef InternEntityDef(Token token, Expression atomTypeExpr)
        {
            string key = token.ToString();
            EntityDef entityDef;
            if (!EntityDictionary.TryGetValue(key, out entityDef))
            {
                entityDef = new EntityDef(token);
                entityDef.AtomTypeExpr = atomTypeExpr;
                EntityDictionary.Add(key, entityDef);
            }
            return entityDef;
        }
        public EntityDef FindEntityDef(Token token)
        {
            string key = token.ToString();
            EntityDef bestEntityDef;
            if (!EntityDictionary.TryGetValue(key, out bestEntityDef))
            {
            }
            return bestEntityDef;
        }
        public PredicateDef InternPredicateDef(Token token)
        {
            string key = token.ToString();
            EntityDef entityDef;
            if (!EntityDictionary.TryGetValue(key, out entityDef))
            {
                entityDef = new PredicateDef(token);
                EntityDictionary.Add(key, entityDef);
            }
            PredicateDef predicateDef = entityDef as PredicateDef;

            if (predicateDef == null)
                throw new Exception();

            return predicateDef;
        }
        public AtomTypeDef InternAtomTypeDef(Token token)
        {
            string key = token.ToString();
            EntityDef atomTypeDef;
            if (!EntityDictionary.TryGetValue(key, out atomTypeDef))
            {
                atomTypeDef = new AtomTypeDef(token, BuiltinDefs.Entity);
                EntityDictionary.Add(key, atomTypeDef);
            }
            return atomTypeDef as AtomTypeDef;
        }
    }
}
