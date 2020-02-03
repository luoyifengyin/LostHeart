using MyGameApplication.Item;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;
using ObjectChache = System.Collections.Concurrent.ConcurrentBag<UnityEngine.GameObject>;

namespace MyGameApplication.ObjectPool {
    //道具对象池，不要直接使用，必须通过ItemManager使用
    public class ItemObjectPool : MonoBehaviour {
        private static ItemObjectPool _instance;
        private Dictionary<int, ObjectChache> objCaches = new Dictionary<int, ObjectChache>();
        private Dictionary<int, int> objCapacitys = new Dictionary<int, int>();      //对象容量
        //private static Dictionary<int, int> objOverflowCnts = new Dictionary<int, int>();   //对象溢出数量

        public static ItemObjectPool Instance {
            get{
                return _instance ?? (_instance = FindObjectOfType<ItemObjectPool>());
            }
        }

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        public void SetCapacityById(int id, int capacity) {
            if (capacity < 1) capacity = 1;
            if (!objCapacitys.ContainsKey(id)) objCapacitys.Add(id, capacity);
            else objCapacitys[id] = capacity;
        }

        public int GetCapacityById(int id) {
            return objCapacitys[id];
        }

        public int GetCntById(int id) {
            return GetCache(id).Count;
        }

        private ObjectChache GetCache(int id) {
            if (!objCaches.ContainsKey(id)) objCaches.Add(id, new ObjectChache());
            return objCaches[id];
        }

        public GameObject Get(int id) {
            var cache = GetCache(id);
            if (!cache.IsEmpty) {
                GameObject obj;
                if (cache.TryTake(out obj)) {
                    obj.SetActive(true);
                    return obj;
                }
            }
            //else if (cache.Count == 1 && objOverflowCnts[id] > 0) {
            //    GameObject obj;
            //    if (cache.TryPeek(out obj)) {
            //        objOverflowCnts[id]--;
            //        return Object.Instantiate(obj);
            //    }
            //}
            var prefab = ItemManager.Instance.itemList[id].gameObject;
            var retObj = Object.Instantiate(prefab);
            return retObj;
        }

        public void Put(int id, GameObject obj) {
            var cache = GetCache(id);
            if (cache.Count < objCapacitys[id]) {
                obj.transform.parent = transform;
                obj.SetActive(false);
                cache.Add(obj);
            }
            else {
                Object.Destroy(obj);
                //objOverflowCnts[id]++;
            }
        }
    }
}
