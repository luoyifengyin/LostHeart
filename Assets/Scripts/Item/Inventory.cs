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

        public virtual int AddItem(int id, int cnt = 1) {
            var itemType = ItemManager.Instance.GetItemTypeById(id);
            var items = GetItemsByType(itemType);
            if (!items.ContainsKey(id)) items.Add(id, 0);
            items[id] += cnt;
            if (items[id] < 0) items[id] = 0;
            return cnt;
        }

        public int GetCntById(int id) {
            var itemType = ItemManager.Instance.GetItemTypeById(id);
            var items = GetItemsByType(itemType);
            if (!items.ContainsKey(id)) items.Add(id, 0);
            return items[id];
        }
    }
}
