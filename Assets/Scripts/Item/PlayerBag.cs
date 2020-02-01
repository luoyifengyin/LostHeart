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

        public void addItem(int id, int cnt, GameObject itemObject = null) {
            base.addItem(id, cnt);
            if (itemObject) ItemManager.Instance.ReleaseItemObjectById(id, itemObject);
        }
    }
}
