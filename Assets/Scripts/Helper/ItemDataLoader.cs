using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Helper {
    public class ItemDataLoader : MonoBehaviour {
        static void LoadItemData() {
            _ = ItemManager.Instance;
            _ = PlayerBag.Instance;
        }

        private void Awake() {
            LoadItemData();
        }
    }
}
