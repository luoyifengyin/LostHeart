using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class MoveReaction : Reaction {
        [SerializeField] private Transform transformToMove;
        [SerializeField] private Vector3 position;

        public override async Task React() {
            transformToMove.position = position;
            await Task.Delay(0);
        }
    }
}
