using MyGameApplication.UI.ItemBar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Item.Inventory {
    public class PlayerBag : Inventory {
        private static PlayerBag _instance;

        public static PlayerBag Instance {
            get => _instance ?? (_instance = new PlayerBag());
        }

        public event Action onItemChange;

        public override int AddItem(int id, ItemType type = ItemType.Prop, int cnt = 1) {
            if (cnt == 0) return 0;
            int preCnt = GetCnt(id, type);
            int capacity = ItemManager.Instance.GetItemCapacity(id, type);
            if (capacity >= 0) cnt = Mathf.Min(cnt, capacity - preCnt);
            base.AddItem(id, type, cnt);
            onItemChange?.Invoke();
            return cnt;
        }
        public int AddProp(int id, int cnt = 1) {
            return AddItem(id, ItemType.Prop, cnt);
        }

        public override int GetCnt(int id, ItemType type = ItemType.Prop) {
            return base.GetCnt(id, type);
        }

        //获取道具容量
        public int GetCapacityByIdAndType(int id, ItemType type = ItemType.Prop) {
            return ItemManager.Instance.GetItemCapacity(id, type);
        }

        //该道具是否已满
        public bool IsFullOfItemByIdAndType(int id, ItemType type = ItemType.Prop) {
            return GetCnt(id, type) >= GetCapacityByIdAndType(id, type);
        }

        public override void ClearItem(int id, ItemType type = ItemType.Prop) {
            base.ClearItem(id, type);
        }
    }
}
