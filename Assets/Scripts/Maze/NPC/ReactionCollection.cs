using MyGameApplication.Maze.NPC.Reactions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC {
    public class ReactionCollection : MonoBehaviour {
        public Reaction[] reactions = new Reaction[0];

        [SerializeField] private bool autoRunOnStart = false;

        public int CurRunIndex { get; set; }

        private void Awake() {
            for(int i = 0;i < reactions.Length; i++) {
                reactions[i].reactionCollection = this;
            }
        }

        private async void Start() {
            if (autoRunOnStart) {
                await React();
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
