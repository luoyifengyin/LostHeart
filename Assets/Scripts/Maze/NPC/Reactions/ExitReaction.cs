using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class ExitReaction : Reaction {
        public override async Task React() {
            await Task.Run(() => reactionCollection.CurRunIndex = -1);
        }
    }
}
