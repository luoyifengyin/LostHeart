using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item.Inventory {
    public class Inventory : IInventory {
        private Dictionary<ItemType, Dictionary<int, int>> m_AllItems = new Dictionary<ItemType, Dictionary<int, int>>();

        public Dictionary<int, int> GetItemsByType(ItemType type) {
            if (!m_AllItems.ContainsKey(type)) {
                m_AllItems.Add(type, new Dictionary<int, int>());
            }
            return m_AllItems[type];
        }

        public virtual int AddItem(int id, ItemType type, int cnt = 1) {
            var items = GetItemsByType(type);
            if (!items.ContainsKey(id)) items.Add(id, cnt);
            else items[id] += cnt;
            if (items[id] < 0) items[id] = 0;
            return cnt;
        }

        public virtual int GetCntByIdAndType(int id, ItemType type) {
            var items = GetItemsByType(type);
            if (!items.ContainsKey(id)) items.Add(id, 0);
            return items[id];
        }
    }
}
