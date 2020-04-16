using MyGameApplication.UI.ItemBar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Item {
    public class PlayerBag : Inventory {
        public static PlayerBag Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        public event Action onItemChange;

        public override int AddItem(int id, ItemType type = ItemType.Prop, int cnt = 1) {
            if (cnt == 0) return 0;
            var itemManager = ItemManager.Instance;
            int preCnt = GetCntByIdAndType(id, type);
            int capacity = itemManager.GetItemCapacity(id, type);
            if (capacity >= 0) cnt = Mathf.Min(cnt, capacity - preCnt);
            base.AddItem(id, type, cnt);
            //var effect = itemManager.propList[id].effect;
            //if (effect != null) effect.OnGained(cnt);
            onItemChange?.Invoke();
            return cnt;
        }

        public override int GetCntByIdAndType(int id, ItemType type = ItemType.Prop) {
            return base.GetCntByIdAndType(id, type);
        }

        //获取道具容量
        public int GetCapacityByIdAndType(int id, ItemType type = ItemType.Prop) {
            return ItemManager.Instance.GetItemCapacity(id, type);
        }

        //该道具是否已满
        public bool IsFullOfItemByIdAndType(int id) {
            return GetCntByIdAndType(id) >= GetCapacityByIdAndType(id);
        }
    }
}
