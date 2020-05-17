using MyGameApplication.Manager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class SceneReaction : Reaction {
        [SerializeField] private string sceneName;

        public override async Task React() {
            await SceneController.LoadScene(sceneName);
        }
    }
}
