using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia.Runtime
{
    public class Process : Scheduler
    {
        //
        public Context Context;
        public event MessageDelegate MessageEvent;
        protected Queue<Message> Posts = new Queue<Message>();
        public List<Proposal> Proposals = new List<Proposal>();
        bool HasProposed = false;
        //
        public Guid Guid;
        public Process Parent;
        public int Depth = 0;
        public const int MaxDepth = 20;
        public List<Process> Processes = new List<Process>();
        public Proposal Proposal;
        public Brain Brain;
        public Expert Expert;
        //
        public Process()
        {
            Init();
        }
        //
        public static Process Create(Brain brain)
        {
            Process process = new Process(brain);
            brain.ProcessCreated(process);
            process.Schedule(brain.Scheduler);
            return process;
        }
        protected Process(Brain brain)
        {
            Brain = brain;
            Init();
        }
        //
        protected Process Create(Proposal proposal)
        {
            Process process = new Process(this, proposal);
            process.Context.Clauses.AddRange(Context.Clauses);
            if (proposal.Context != null)
                process.ImportContext(proposal.Context);
            //context.Post(proposal.Message);
            proposal.Callback(process, proposal.Expert, proposal.Message);
            Processes.Add(process);
            Brain.ProcessCreated(process);
            process.Schedule(Process);
            return process;
        }
        protected Process(Process parent, Proposal proposal)
        {
            Parent = parent;
            Depth = parent.Depth + 1;
            Proposal = proposal;
            Process = parent.Process;
            Brain = parent.Brain;
            Expert = parent.Expert;
            Init();
        }
        //
        private void Init()
        {
            Guid = Guid.NewGuid();
            Context = new Context();
            MessageEvent += OnPost;
        }
        //
        public override void Update()
        {
            while (Posts.Count != 0 && IsActive)
            {
                MessageEvent(Posts.Dequeue());
            }
            base.Update();
        }
        public override bool NeedsSchedule()
        {
            if (Posts.Count == 0 && Proposals.Count == 0 && base.NeedsSchedule())
                return true;
            else
                return false;
        }
        public override bool NeedsUpdating()
        {
            if (Posts.Count != 0 || base.NeedsUpdating())
                return true;
            else
                return false;
        }
        //
        public void PushExpert(Expert expert)
        {
            expert.Parent = this.Expert;
            this.Expert = expert;
        }
        public Expert PopExpert()
        {
            Expert = Expert.Parent;
            return Expert;
        }
        //
        public void Post(Message post)
        {
            MaybeSchedule();
            Posts.Enqueue(post);
        }
        public void OnPost(Message message)
        {
            switch (message.Kind)
            {
                case MessageKind.Add:
                    Context.Add(message.Clause);
                    Expert.OnPost(this, message);
                    break;
                case MessageKind.Remove:
                    Context.Remove(message.Clause);
                    Expert.OnPost(this, message);
                    break;
                case MessageKind.Modify:
                    Clause replacedClause = Context.Modify(message.Clause);
                    if(replacedClause != null)
                        Expert.OnPost(this, new Message(MessageKind.Remove, null, null, replacedClause));
                    Expert.OnPost(this, new Message(MessageKind.Add, null, null, message.Clause));
                    break;
                case MessageKind.Callback:
                    MessageCallback callback = (MessageCallback)message.Clause.Object;
                    callback(this, Expert, message);
                    break;
                default:
                    Expert.OnPost(this, message);
                    break;
            }
            //Expert.OnPost(this, message);
        }
        /*public void Propose(Message msg, Context context)
        {
            Proposal proposal = new Proposal();
            proposal.Message = msg;
            proposal.Context = context;
            Propose(proposal);
        }*/
        public void Propose(Proposal proposal)
        {
            MaybeSchedule();
            Proposals.Add(proposal);
            HasProposed = true;
        }
        public void ImportContext(Context context)
        {
            foreach (var clause in context.Clauses)
            {
                Post(new Message(null, clause));
            }
        }
        //
        protected override void JumpStart()
        {
            Decide();
            base.JumpStart();
        }
        public void Decide()
        {
            if (StallCount == 1 && !HasProposed)
            {
                Impasse();
                return;
            }
            //else
            if (Depth >= MaxDepth)
                return;
            //
            foreach (var proposal in Proposals)
            {
                Process context = Create(proposal);
            }
            Proposals.Clear();
        }
        public void Impasse()
        {
            Post(Message.NilMessage);
            Status = TaskStatus.Active;
        }
    }
}
