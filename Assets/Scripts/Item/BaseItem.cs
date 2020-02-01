using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    public abstract class BaseItem : MonoBehaviour {
        private static int s_Uid = 0;

        public int Uid { get; private set; }    //道具的唯一标识id，所有道具都拥有不同的uid
        public int ItemId { get; set; }         //道具id，跟ItemList.json文件中的id一致

        private void Awake() {
            Uid = ++s_Uid;
        }

        //当玩家获得该道具时触发
        public virtual void OnCollected(int cnt) { }

        //使用条件
        public virtual bool UseCondition() {
            return true;
        }

        //道具效果
        public virtual void PayLoad() {
            if (!UseCondition()) return;
        }

        //道具过期/丢弃时调用
        public void expire() {
            ItemManager.Instance.ReleaseItemObjectById(ItemId, gameObject);
        }
    }
}
