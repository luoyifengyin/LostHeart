using MyGameApplication.UI.ItemBar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Item {
    public class PlayerBag : Inventory {
        private static PlayerBag _instance;
        public event Action onItemChange;

        public static PlayerBag Instance {
            get {
                return _instance ?? (_instance = FindObjectOfType<PlayerBag>());
            }
        }

        public override void AddItem(int id, int cnt = 1) {
            if (cnt == 0) return;
            var itemManager = ItemManager.Instance;
            int preCnt = GetCntById(id);
            int capacity = itemManager.GetItemCapacityById(id);
            if (capacity >= 0) cnt = Mathf.Min(cnt, capacity - preCnt);
            base.AddItem(id, cnt);
            GameObject go = itemManager.itemList[id].prefab;
            if (go) {
                BaseItem item = go.GetComponent<BaseItem>();
                if (item) item.OnGained(GetCntById(id) - preCnt);
            }
            onItemChange?.Invoke();
        }

        //获取道具容量
        public int GetCapacityById(int id) {
            return ItemManager.Instance.GetItemCapacityById(id);
        }

        //该道具是否已满
        public bool IsFullOfItemById(int id) {
            return GetCntById(id) >= GetCapacityById(id);
        }
    }
}
