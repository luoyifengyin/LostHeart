using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    public abstract class BaseItem : MonoBehaviour {
        private static PlayerBag _inventory;
        private static int s_Uid = 0;

        public int Uid { get; private set; }
        public int ItemId { get; set; }

        private void Awake() {
            if (!_inventory) _inventory = PlayerBag.Instance;
            Uid = ++s_Uid;
        }

        protected virtual void OnCollected() { }

        public virtual bool UseCondition() {
            return true;
        }

        public virtual void PayLoad() {
            if (!UseCondition()) return;
        }

        public void expire() {
            ItemManager.Instance.ReleaseItemObjectById(ItemId, gameObject);
        }
    }
}
