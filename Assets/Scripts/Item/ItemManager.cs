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
            //ItemObjectPool objectPool = ItemObjectPool.Instance;
            var itemEffectObj = new GameObject("Item Effects");
            GameObject.DontDestroyOnLoad(itemEffectObj);
            for(int i = 1;i < _instance.propList.Length; i++) {
                var prop = _instance.propList[i];
                prop.sprite = Resources.Load<Sprite>("Items/Props/Sprites/P" + i);
                prop.prefab = Resources.Load<GameObject>("Items/Props/Prefabs/P" + i);
                if (prop.prefab) {
                    prop.effect = Component.Instantiate(prop.prefab.GetComponent<PropEffect>(),
                        itemEffectObj.transform);
                    var components = prop.effect.gameObject.GetComponents<Component>();
                    foreach(var component in components) {
                        if (!(component is Transform) && !(component is PropEffect)) {
                            Component.Destroy(component);
                        }
                    }
                }
                //int capacity = _instance.propList[i].objectPoolCapacity;
                //objectPool.SetCapacityById(i, capacity);
                //int initCnt = _instance.propList[i].objectPoolInitCnt;
                //objectPool.CreateSpecifiedObjectsById(i, initCnt);
            }
            return _instance;
        }

        //道具gameObject的生成和释放，必须使用ItemManager的接口，通过对象池进行管理，必须成对使用，需要手动释放，否则会造成内存泄漏
        //public GameObject CreateItemObjectById(int id) {
        //    GameObject obj = ItemObjectPool.Instance.Get(id);
        //    if (!obj) return null;
        //    //obj.SetActive(false);
        //    BaseItem item = obj.GetComponent<BaseItem>();
        //    if (item) item.ItemId = id;
        //    return obj;
        //}
        //public void ReleaseItemObjectById(int id, GameObject obj) {
        //    if (!obj) return;
        //    ItemObjectPool.Instance.Put(id, obj);
        //}

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
            if (propList[id].effect) propList[id].effect.Payload();
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
