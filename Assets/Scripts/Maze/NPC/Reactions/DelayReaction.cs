using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class DelayReaction : Reaction {
        [SerializeField] private int m_Milliseconds = 0;

        public override async Task React() {
            await Task.Delay(m_Milliseconds);
        }
    }
}
