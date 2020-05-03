using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class AnotherReaction : Reaction {
        [SerializeField] private ReactionCollection m_AnotherReactionCollection;
        [SerializeField] private bool m_Asyn = false;

        public override async Task React() {
            if (!m_AnotherReactionCollection) return;
            if (m_Asyn) _ = m_AnotherReactionCollection.React();
            else await m_AnotherReactionCollection.React();
        }
    }
}
