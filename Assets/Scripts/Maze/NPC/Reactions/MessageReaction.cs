using MyGameApplication.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    public class MessageReaction : Reaction {
        [SerializeField] private string m_Message;

        public override async Task React() {
            await MessageBox.Instance.Show(m_Message);
        }
    }
}
