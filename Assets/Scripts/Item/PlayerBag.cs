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
            var itemManager = ItemManager.Instance;
            int curCnt = GetCntById(id);
            int capacity = itemManager.GetItemCapacityById(id);
            cnt = Mathf.Min(cnt, capacity - curCnt);
            base.AddItem(id, cnt);
            GameObject go = itemManager.itemList[id].prefab;
            if (go) {
                BaseItem item = go.GetComponent<BaseItem>();
                if (item) item.OnGained(cnt);
            }
        }
    }
}
