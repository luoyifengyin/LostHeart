using MyGameApplication.Maze.NPC.Reactions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC {
    public class ReactionCollection : MonoBehaviour {
#if UNITY_EDITOR
        public string description;
#endif
        public Reaction[] reactions = new Reaction[0];

        public int CurRunIndex { get; set; }

        private void Awake() {
            for(int i = 0;i < reactions.Length; i++) {
                reactions[i].reactionCollection = this;
            }
        }

        public async Task React() {
            for(CurRunIndex = 0; CurRunIndex < reactions.Length; CurRunIndex++) {
                await reactions[CurRunIndex].React();
                if (CurRunIndex < 0) return;
            }
        }
    }
}
