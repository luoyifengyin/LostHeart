using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    public class PlayerBag : Inventory {
        private static PlayerBag _instance;

        public static PlayerBag Instance {
            get {
                return _instance ?? (_instance = FindObjectOfType<PlayerBag>());
            }
        }

        public override void addItem(int id, int cnt = 1) {
            base.addItem(id, cnt);
            GameObject go = ItemManager.Instance.itemList[id].gameObject;
            BaseItem item = null;
            if (go) item = go.GetComponent<BaseItem>();
            if (item) item.OnCollected(cnt);
        }
    }
}
