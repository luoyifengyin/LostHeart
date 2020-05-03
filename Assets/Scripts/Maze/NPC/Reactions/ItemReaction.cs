using MyGameApplication.Item.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class ItemReaction : Reaction {
        [SerializeField] private int m_ItemId;
        [SerializeField] private int m_GainCnt;

        public override async Task React() {
            PlayerBag.Instance.AddProp(m_ItemId, m_GainCnt);
        }
    }
}
