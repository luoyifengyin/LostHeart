using MyGameApplication.Maze.NPC.Reactions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC {
    public class ReactionCollection : MonoBehaviour {
        public Reaction[] reactions = new Reaction[0];

        [SerializeField] private bool autoRunOnStart = false;

        private int m_CurRunIdx = 0;
        public int CurRunIndex {
            get => m_CurRunIdx;
            set {
                m_CurRunIdx = value;
                m_GotoFlag = true;
            }
        }
        private bool m_GotoFlag = false;

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
            m_CurRunIdx = 0;
            while (m_CurRunIdx < reactions.Length) {
                await reactions[m_CurRunIdx].React();
                if (m_CurRunIdx < 0) return;
                if (!m_GotoFlag) m_CurRunIdx++;
                else m_GotoFlag = false;
            }
        }
    }
}
