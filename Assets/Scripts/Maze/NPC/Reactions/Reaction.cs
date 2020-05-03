using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    [Serializable]
    public abstract class Reaction : ScriptableObject {
        [HideInInspector] public ReactionCollection reactionCollection;

        public abstract Task React();
    }
}
