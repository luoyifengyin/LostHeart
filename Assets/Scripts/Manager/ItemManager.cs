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
            public int id;                              //道具id（必需）
            public ItemType type;                       //道具类型（必需）
            public string name;                         //道具名称
            public string description;                  //道具描述
            public int capacity = -1;                   //道具容量（-1代表无上限）
            public string uiPath;                       //道具资源路径（必需）
            public int objectPoolCapacity = 100;        //道具对象池容量（至少为1）
            [NonSerialized] private GameObject mPrefab; //外部使用该值时必须只读，且不能持有
            [NonSerialized] private Sprite mSprite;     //道具的精灵图，用于UI显示
            public GameObject gameObject {
                get {
                    if (!mPrefab) {
                        mPrefab = _instance.CreateItemObjectById(id);
                    }
                    return mPrefab;
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
            ItemObjectPool objectPool = ItemObjectPool.Instance;
            for(int i = 1;i < _instance.itemList.Length; i++) {
                int capacity = _instance.itemList[i].objectPoolCapacity;
                objectPool.SetCapacityById(i, capacity);
            }
            return _instance;
        }

        //道具gameObject的生成和释放，必须使用ItemManager的接口，通过对象池进行管理，必须成对使用，需要手动释放，否则会造成内存泄漏
        public GameObject CreateItemObjectById(int id) {
            GameObject obj = ItemObjectPool.Instance.Get(id);
            //obj.SetActive(false);
            BaseItem item = obj.GetComponent<BaseItem>();
            if (item) item.ItemId = id;
            return obj;
        }
        public void ReleaseItemObjectById(int id, GameObject obj) {
            if (!obj) return;
            ItemObjectPool.Instance.Put(id, obj);
        }

        public ItemType GetItemTypeById(int id) {
            return itemList[id].type;
        }
        public string GetItemNameById(int id) {
            return itemList[id].name;
        }
        public string GetItemDescById(int id) {
            return itemList[id].description;
        }
        public int GetItemCapacityById(int id) {
            return itemList[id].capacity;
        }
        public Sprite GetItemSpriteById(int id) {
            return itemList[id].sprite;
        }

        public GameObject UseItemEffectById(int id) {
            GameObject obj = CreateItemObjectById(id);
            if (obj) {
                BaseItem itemController = obj.GetComponent<BaseItem>();
                if (itemController) itemController.Payload();
            }
            return obj;
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
