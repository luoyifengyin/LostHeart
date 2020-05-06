using MyGameApplication.Data;
using MyGameApplication.Data.Saver;
using MyGameApplication.Manager;
using MyGameApplication.UI.ItemBar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Item.Inventory {
    public class PlayerBag : Inventory, IPersistentSaver {
        private static PlayerBag _instance;

        public static PlayerBag Instance {
            get => _instance ?? (_instance = new PlayerBag());
        }

        public event Action onItemChange;

        private PersistentSaveData gameData;

        private PlayerBag() {
            gameData = GameManager.Instance.GameData;
            GameManager.Instance.OnSave += Save;
            GameManager.Instance.OnLoad += Load;
        }

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

        public void Save() {
            var items = GetItemsByType(ItemType.Prop);
            foreach(var item in items) {
                gameData.SavePropCnt(item.Key, item.Value);
            }
        }

        public void Load() {
            List<int> propCntList = gameData.LoadPropCnt();
            for(int i = 0;i < propCntList.Count; i++) {
                if (propCntList[i] > 0) {
                    AddProp(i, propCntList[i]);
                }
            }
        }
    }
}
