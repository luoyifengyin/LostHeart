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
        public static ItemObjectPool Instance { get; private set; }

        private Dictionary<int, GameObject> m_Prefabs = new Dictionary<int, GameObject>();
        private Dictionary<int, ObjectChache> m_UnusedCaches = new Dictionary<int, ObjectChache>();
        private Dictionary<int, int> m_ObjCapacitys = new Dictionary<int, int>();                   //对象池容量
        //private static Dictionary<int, int> objOverflowCnts = new Dictionary<int, int>();         //对象溢出数量

        private void Awake() {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }

        public int GetCapacityById(int id) {
            return m_ObjCapacitys[id];
        }
        public void SetCapacityById(int id, int capacity) {
            if (!m_ObjCapacitys.ContainsKey(id)) m_ObjCapacitys.Add(id, capacity);
            else m_ObjCapacitys[id] = capacity;
        }
        public bool IsPoolFullById(int id) {
            int capacity = GetCapacityById(id);
            if (capacity < 0) return false;
            else return (GetUnusedCache(id).Count >= capacity);
        }

        private GameObject CreateObject(int id) {
            if (!m_Prefabs.ContainsKey(id))
                m_Prefabs.Add(id, ItemManager.Instance.itemList[id].prefab);
            if (!m_Prefabs[id]) return null;
            var obj = Instantiate(m_Prefabs[id]);
            obj.transform.SetParent(transform);
            return obj;
        }

        public void CreateSpecifiedObjectsById(int id, int cnt) {
            var cache = GetUnusedCache(id);
            for (int i = 0; i < cnt; i++) {
                cache.Add(CreateObject(id));
            }
        }

        public int GetCacheCntById(int id) {
            return GetUnusedCache(id).Count;
        }

        private ObjectChache GetUnusedCache(int id) {
            if (!m_UnusedCaches.ContainsKey(id)) m_UnusedCaches.Add(id, new ObjectChache());
            return m_UnusedCaches[id];
        }

        public GameObject Get(int id) {
            var cache = GetUnusedCache(id);
            GameObject retObj;
            if (!cache.IsEmpty && cache.TryTake(out retObj)) {
                retObj.SetActive(true);
            }
            //else if (cache.Count == 1 && objOverflowCnts[id] > 0) {
            //    GameObject obj;
            //    if (cache.TryPeek(out obj)) {
            //        objOverflowCnts[id]--;
            //        retObj = Object.Instantiate(obj);
            //        retObj.SetActive(true);
            //    }
            //}
            else {
                retObj = CreateObject(id);
            }
            return retObj;
        }

        public void Put(int id, GameObject obj) {
            if (m_Prefabs.ContainsKey(id) && m_Prefabs[id] == obj) return;
            var cache = GetUnusedCache(id);
            if (!IsPoolFullById(id)) {
                obj.transform.parent = transform;
                obj.SetActive(false);
                cache.Add(obj);
            }
            else {
                Destroy(obj);
                //objOverflowCnts[id]++;
            }
        }

        public void ClearPoolById(int id) {
            var unusedCache = GetUnusedCache(id);
            foreach(var obj in unusedCache) Destroy(obj);
            m_UnusedCaches.Remove(id);
        }
    }
}
