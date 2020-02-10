using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Mia
{
    [Flags]
    public enum AtomFlag : int
    {
        None = 0,
        Atom = 1,
        EntityFlag = 2,
        Clause = 4,
        Goal = 8,
        PerformFlag = 16,
        AchieveFlag = 32,
        QueryFlag = 64,
        MaintainFlag = 128,
        //
        Entity = Atom | EntityFlag,
        Belief = Atom | Clause,
        Perform = Clause | Goal | PerformFlag,
        Achieve = Clause | Goal | AchieveFlag,
        Query = Clause | Goal | QueryFlag,
        Maintain = Clause | Goal | MaintainFlag,

        //Messaging

        Attempt = 256,
        Add = 512,
        Remove = 1024,
        Modify = Add | Remove,
        Negate = 2048,
        Callback = 4096,

        //Matching

        SubjectX = 8192,
        PredicateX = 16384,
        ObjectX = 32768,
    }
}
