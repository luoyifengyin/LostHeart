using MyGameApplication.Item;
using MyGameApplication.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    public class Inventory : MonoBehaviour {
        private Dictionary<ItemType, Dictionary<int, int>> m_AllItems = new Dictionary<ItemType, Dictionary<int, int>>();

        public Dictionary<int, int> GetItemsByType(ItemType type) {
            if (!m_AllItems.ContainsKey(type)) {
                m_AllItems.Add(type, new Dictionary<int, int>());
            }
            return m_AllItems[type];
        }

        public virtual void addItem(int id, int cnt = 1) {
            ItemManager itemManager = ItemManager.Instance;
            ItemManager.ItemBean item = itemManager.itemList[id];
            var items = GetItemsByType(item.type);
            if (!items.ContainsKey(id)) items.Add(id, 0);
            items[id] += cnt;
        }
    }
}
