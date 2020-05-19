using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    enum TalkType {
        say,
        choice,
    }

    [Serializable]
    public class TalkReaction : Reaction {
        [SerializeField] private new string name = null;
        [Multiline] [SerializeField] private string say;

        public override async Task React() {
            string content;
            if (!string.IsNullOrEmpty(name))
                content = name + ":\n" + say;
            else content = say;
            await Dialogue.DialogBox.ShowDialogue(content);
        }
    }
}
