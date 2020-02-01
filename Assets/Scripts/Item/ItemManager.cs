using MyGameApplication.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MyGameApplication.Item {
    public enum ItemType {
        Prop,
    }

    [Serializable]
    public class ItemManager {
        private static ItemManager _instance;

        [Serializable]
        public struct Setting {
            public string prefabRootPath;
            public string spriteRootPath;
        }

        [Serializable]
        public class ItemBean {
            public int id;
            public ItemType type;
            public string name;
            public string description;
            public int capacity = -1;
            public string uiPath;
            public int objectPoolCapacity = 100;
            [NonSerialized] public GameObject mGameObject;
            [NonSerialized] public Sprite mSprite;
            public GameObject gameObject {
                get {
                    if (!mGameObject) {
                        string path = _instance.setting.prefabRootPath + uiPath;
                        mGameObject = Resources.Load<GameObject>(path);
                    }
                    return mGameObject;
                }
            }
            public Sprite sprite {
                get {
                    if (!mSprite) {
                        string path = _instance.setting.spriteRootPath + uiPath;
                        mSprite = Resources.Load<Sprite>(path);
                    }
                    return mSprite;
                }
            }
        }

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

        public Setting setting;
        public ItemBean[] itemList;
        public ItemInfo infos;

        public static ItemManager Instance {
            get {
                return _instance ?? Initialize();
            }
        }
        private static ItemManager Initialize() {
            TextAsset textAsset = Resources.Load<TextAsset>("Text/ItemList");
            _instance = JsonUtility.FromJson<ItemManager>(textAsset.text);
            for(int i = 1;i < _instance.itemList.Length; i++) {
                int capacity = _instance.itemList[i].capacity;
                ItemObjectPool.Instance.SetCapacityById(i, capacity);
            }
            return _instance;
        }

        public GameObject CreateItemObjectById(int id) {
            GameObject obj = ItemObjectPool.Instance.Get(id);
            obj.SetActive(false);
            BaseItem item = obj.GetComponent<BaseItem>();
            if (item) item.ItemId = id;
            return obj;
        }
        public void ReleaseItemObjectById(int id, GameObject obj) {
            ItemObjectPool.Instance.Put(id, obj);
        }

        public Sprite GetSpriteById(int id) {
            return itemList[id].sprite;
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
            int rand = Random.Range(0, preSum[len-1]);
            int idx = Array.BinarySearch(preSum, rand + 1);
            if (idx < 0) idx = ~idx;
            return arr[idx].id;
        }

        public int GetRandomCarItemId() {
            return GetRandomItemIdByArray(infos.carRacingItemList, infos.carRacingItemPreSum);
        }
    }
}
