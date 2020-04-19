using MyGameApplication.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MyGameApplication.Item {
    [Serializable]
    public class ItemManager {
        private static ItemManager _instance;

        [Serializable]
        public class ItemInfo {
            [Serializable]
            public struct InfoBean {
                public int id;
                public int weight;
            }
            public InfoBean[] carRacingItemList;
            [NonSerialized] public int[] carRacingItemPreSum;
        }

        public Prop[] propList;
        public ItemInfo infos;

        private Dictionary<ItemType, BaseItem[]> items = new Dictionary<ItemType, BaseItem[]>();

        public static ItemManager Instance {
            get {
                return _instance ?? Initialize();
            }
        }

        private static ItemManager Initialize() {
            TextAsset textAsset = Resources.Load<TextAsset>("Text/ItemList");
            _instance = JsonUtility.FromJson<ItemManager>(textAsset.text);

            _instance.items.Add(ItemType.Prop, _instance.propList);

            for(int i = 1;i < _instance.propList.Length; i++) {
                var prop = _instance.propList[i];
                prop.sprite = Resources.Load<Sprite>("Items/Props/Sprites/P" + i);
                prop.prefab = Resources.Load<GameObject>("Items/Props/Prefabs/P" + i);
                Type type = Type.GetType("MyGameApplication.Item.Effect.P" + i);
                if (type != null) {
                    prop.effect = Activator.CreateInstance(type) as PropEffect;
                }
            }
            return _instance;
        }

        public string GetItemName(int id, ItemType type) {
            return items[type][id].name;
        }
        public string GetItemDesc(int id, ItemType type) {
            return items[type][id].description;
        }
        public int GetItemCapacity(int id, ItemType type) {
            return items[type][id].capacity;
        }
        public Sprite GetItemSprite(int id, ItemType type) {
            return items[type][id].sprite;
        }
        public GameObject GetItemPrefab(int id, ItemType type) {
            return items[type][id].prefab;
        }

        public void UsePropEffect(int id) {
            if (propList[id].effect != null) propList[id].effect.Payload();
        }

        private int GetRandomItemIdByArray(ItemInfo.InfoBean[] arr, int[] preSum) {
            int len = arr.Length;
            if (preSum == null) {
                preSum = new int[len];
                int sum = 0;
                for(int i = 0;i < arr.Length; i++) {
                    sum += arr[i].weight;
                    preSum[i] = sum;
                }
            }
            int rand = Random.Range(0, preSum[len - 1]);
            int idx = Array.BinarySearch(preSum, rand + 1);
            if (idx < 0) idx = ~idx;
            return arr[idx].id;
        }

        public int GetRandomCarItemId() {
            return GetRandomItemIdByArray(infos.carRacingItemList, infos.carRacingItemPreSum);
        }
    }
}
