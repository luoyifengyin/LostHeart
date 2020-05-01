using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class AnotherReaction : Reaction {
        [SerializeField] private ReactionCollection m_AnotherReactionCollection;
        [SerializeField] private bool m_EndOnThisReactionEnd = false;

        public override async Task React() {
            await m_AnotherReactionCollection.React();
            if (m_EndOnThisReactionEnd)
                reactionCollection.CurRunIndex = -1;
        }
    }
}
