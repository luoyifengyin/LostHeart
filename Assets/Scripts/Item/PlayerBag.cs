using MyGameApplication.UI.ItemBar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Item {
    public class PlayerBag : Inventory {
        public static PlayerBag Instance { get; private set; }

        public event Action onItemChange;

        private void Awake() {
            Instance = this;
        }

        public override int AddItem(int id, int cnt = 1) {
            if (cnt == 0) return 0;
            var itemManager = ItemManager.Instance;
            int preCnt = GetCntById(id);
            int capacity = itemManager.GetItemCapacityById(id);
            if (capacity >= 0) cnt = Mathf.Min(cnt, capacity - preCnt);
            base.AddItem(id, cnt);
            var effect = itemManager.itemList[id].effect;
            if (effect) effect.OnGained(cnt);
            onItemChange?.Invoke();
            return cnt;
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
