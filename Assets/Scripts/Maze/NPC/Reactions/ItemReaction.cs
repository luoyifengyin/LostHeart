using MyGameApplication.Item.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class ItemReaction : Reaction {
        [SerializeField] private int m_ItemId;
        [SerializeField] private int m_GainCnt = 0;

        public override async Task React() {
            await Task.Run(() => PlayerBag.Instance.AddProp(m_ItemId, m_GainCnt));
        }
    }
}
