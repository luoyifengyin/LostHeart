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

        public override void AddItem(int id, int cnt = 1) {
            if (cnt == 0) return;
            base.AddItem(id, cnt);
            GameObject go = ItemManager.Instance.itemList[id].gameObject;
            if (go) {
                BaseItem item = go.GetComponent<BaseItem>();
                if (item) item.OnGained(cnt);
            }
        }
    }
}
