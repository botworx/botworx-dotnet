using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Expert
    {
        public Expert Parent;
        public MessageCallback Boot;
        public List<Trigger> Triggers = new List<Trigger>();
        //
        public void AddTrigger(Trigger trigger)
        {
            Triggers.Add(trigger);
        }
        public IEnumerable<Trigger> MatchTrigger(Message msg)
        {
            foreach (Trigger trigger in Triggers)
            {
                if (!trigger.MessagePattern.Match(msg))
                    continue;
                //else
                yield return trigger;
            }
        }
        //
        public void Post(Process process, Message message)
        {
            process.Post(message);
        }
        public void OnPost(Process process, Message message)
        {
            foreach (Trigger trigger in MatchTrigger(message))
            {
                trigger.Callback(process, this, message);
            }
            if (Parent != null)
                Parent.OnPost(process, message);
        }
        //
        public void Propose(Process process, Message message, Context importContext)
        {
            foreach (Trigger trigger in MatchTrigger(message))
            {
                Proposal proposal = new Proposal();
                proposal.Message = message;
                proposal.Expert = this;
                proposal.Callback = trigger.Callback;
                proposal.Context = importContext;
                process.Propose(proposal);
            }
            if (Parent != null)
                Parent.Propose(process, message, importContext);
        }
        public void Retract(Process process, Clause clause)
        {
            Post(process, new Message(MessageKind.Remove, null, null, clause));
        }
        //
        protected void Begin(MentalTask task)
        {
            Clause clause = task.Message.Clause;
            if (clause != null)
            {
                Entity contextEntity = (Entity)clause[Ent_context];
                if (contextEntity != null)
                {
                    Context importContext = (Context)contextEntity.Value;
                    if (importContext != null)
                        task.Process.ImportContext(importContext);
                }
            }
        }
        public TaskStatus Return(Process process, Message message)
        {
            return TaskStatus.Succeeded;
        }
        public TaskStatus Succeed(Process context, Message message)
        {
            if(message.Kind == MessageKind.Attempt && message.Clause != null)
                Retract(context, message.Clause);
            return TaskStatus.Succeeded;
        }
        public TaskStatus Fail(Process context, Message message)
        {
            return TaskStatus.Failed;
        }
        public TaskStatus Throw(Process context, Message message)
        {
            context.CanJumpStart = false;
            return TaskStatus.Failed;
        }
        //Types
        public static AtomType Ent_Atom = new AtomType("Atom", null, AtomFlag.Atom);
        public static AtomType Ent_Entity = new AtomType("Entity", Ent_Atom, AtomFlag.Entity);
        public static AtomType Ent_Clause = new AtomType("Clause", Ent_Atom, AtomFlag.Clause);
        public static AtomType Ent_Belief = new AtomType("Belief", Ent_Clause, AtomFlag.Belief);
        public static AtomType Ent_Goal = new AtomType("Goal", Ent_Clause, AtomFlag.Goal);
        public static AtomType Ent_Perform = new AtomType("Perform", Ent_Goal, AtomFlag.Perform);
        public static AtomType Ent_Achieve = new AtomType("Achieve", Ent_Goal, AtomFlag.Achieve);
        public static AtomType Ent_Query = new AtomType("Query", Ent_Goal, AtomFlag.Query);
        public static AtomType Ent_Maintain = new AtomType("Maintain", Ent_Goal, AtomFlag.Maintain);
        //Predicates
        public static AtomType Ent_context = new AtomType("context");
        public static AtomType Ent_status = new AtomType("status");
        public static AtomType Ent_callback = new AtomType("callback");
        //Entities
        public static Entity Ent_NIL = new Entity("NIL");
        public static Entity Ent_BLANK = new Entity("_");
        public static Entity Ent_Self = new Entity("Self");
    }
}
