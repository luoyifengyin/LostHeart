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
        private Dictionary<int, GameObject> m_Prefabs = new Dictionary<int, GameObject>();
        private Dictionary<int, ObjectChache> m_UnusedCaches = new Dictionary<int, ObjectChache>();
        private Dictionary<int, HashSet<GameObject>> m_UsingCaches = new Dictionary<int, HashSet<GameObject>>();
        private Dictionary<int, int> m_ObjCapacitys = new Dictionary<int, int>();                   //对象池容量
        //private static Dictionary<int, int> objOverflowCnts = new Dictionary<int, int>();         //对象溢出数量

        public static ItemObjectPool Instance {
            get{
                return _instance ?? (_instance = FindObjectOfType<ItemObjectPool>());
            }
        }

        private void Awake() {
            DontDestroyOnLoad(transform.root.gameObject);
        }

        public void SetCapacityById(int id, int capacity) {
            if (capacity < 0) capacity = 0;
            if (!m_ObjCapacitys.ContainsKey(id)) m_ObjCapacitys.Add(id, capacity);
            else m_ObjCapacitys[id] = capacity;
        }

        public void CreateSpecifiedObjectById(int id, int cnt) {
            var prefab = GetPrefab(id);
            var cache = GetUnusedCache(id);
            for (int i = 0; i < cnt; i++) {
                var obj = Instantiate(prefab);
                obj.transform.parent = transform;
                cache.Add(obj);
            }
        }

        public int GetCapacityById(int id) {
            return m_ObjCapacitys[id];
        }

        public int GetCacheCntById(int id) {
            return GetUnusedCache(id).Count + GetUsingCache(id).Count;
        }

        private ObjectChache GetUnusedCache(int id) {
            if (!m_UnusedCaches.ContainsKey(id)) m_UnusedCaches.Add(id, new ObjectChache());
            return m_UnusedCaches[id];
        }
        private HashSet<GameObject> GetUsingCache(int id) {
            if (!m_UsingCaches.ContainsKey(id)) m_UsingCaches.Add(id, new HashSet<GameObject>());
            return m_UsingCaches[id];
        }

        private GameObject GetPrefab(int id) {
            if (!m_Prefabs.ContainsKey(id))
                m_Prefabs.Add(id, ItemManager.Instance.itemList[id].prefab);
            return m_Prefabs[id];
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
                var prefab = GetPrefab(id);
                retObj = Object.Instantiate(prefab);
                retObj.transform.parent = transform;
            }
            GetUsingCache(id).Add(retObj);
            return retObj;
        }

        public void Put(int id, GameObject obj) {
            if (m_Prefabs.ContainsKey(id) && m_Prefabs[id] == obj) return;
            var cache = GetUnusedCache(id);
            var usingSet = GetUsingCache(id);
            usingSet.Remove(obj);
            if (cache.Count < GetCapacityById(id)) {
                obj.transform.parent = transform;
                obj.SetActive(false);
                cache.Add(obj);
            }
            else {
                Object.Destroy(obj);
                //objOverflowCnts[id]++;
            }
        }

        public void ClearPoolById(int id) {
            var unusedCache = GetUnusedCache(id);
            foreach(var obj in unusedCache) Destroy(obj);
            //m_UnusedCaches.Remove(id);
            var usingCache = GetUsingCache(id);
            foreach(var obj in usingCache)  Destroy(obj);
            //m_UsingCaches.Remove(id);
        }
    }
}
