using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class NumberInputReaction : Reaction {
        [SerializeField] private string correctAnswer;
        [SerializeField] private int correctGoto = -1;
        [SerializeField] private int wrongGoto = -1;
        [SerializeField] private int cancelGoto = -1;

        public override async Task React() {
            await NumberInputPanel.Instance.Show();
                
        }
    }
}
