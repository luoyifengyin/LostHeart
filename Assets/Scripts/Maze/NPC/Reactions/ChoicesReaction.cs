using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.Maze.NPC.Reactions {
    [Serializable]
    public class Branch {
        public enum BranchType {
            ReactionCollection,
            Goto,
        }

        public string name;

        public int gotoIdx;
    }

    public class ChoicesReaction : Reaction {
        [SerializeField] private Branch[] choiceBranches;

        private void OnEnable() {
            if (choiceBranches == null) {
                choiceBranches = new Branch[2];
                choiceBranches[0] = new Branch {
                    name = "是"
                };
                choiceBranches[1] = new Branch {
                    name = "否"
                };
            }
        }

        public override async Task React() {
            if (choiceBranches == null || choiceBranches.Length == 0) return;

            string[] choiceNames = new string[choiceBranches.Length];
            for(int i = 0;i < choiceBranches.Length; i++) {
                choiceNames[i] = choiceBranches[i].name;
            }

            int selId = await Dialogue.DialogChoices.DisplayChoices(choiceNames);

            reactionCollection.CurRunIndex = choiceBranches[selId].gotoIdx;
        }
    }
}
