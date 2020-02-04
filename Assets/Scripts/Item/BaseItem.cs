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

        //当玩家获得（失去）该道具时触发
        public virtual void OnGained(int cnt) { }

        //使用条件
        public virtual bool Condition() {
            return true;
        }

        //道具效果
        protected virtual void Operation() { }

        //使用道具
        public void Payload() {
            if (!Condition()) return;
            Operation();
        }

        //道具效果失效时调用
        public virtual void Expire() { }

        //中断该道具的所有效果和特效
        public virtual void Stop() {
            Expire();
        }

        protected void Release() {
            ItemManager.Instance.ReleaseItemObjectById(ItemId, gameObject);
        }
    }
}
