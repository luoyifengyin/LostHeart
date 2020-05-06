using MyGameApplication.Data;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class ConditionReaction : Reaction {
        [SerializeField] public string key;
        [SerializeField] public bool satisfied;

        public override async Task React() {
            await Task.Run(() => GameManager.Instance.GameData.Save(key, satisfied));
        }
    }
}
