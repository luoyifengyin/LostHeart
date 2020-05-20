using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class ActiveReaction : Reaction {
        [SerializeField] private GameObject gameObject;
        [SerializeField] private bool active;

        public override async Task React() {
            gameObject.SetActive(active);
            await Task.Delay(0);
        }
    }
}
