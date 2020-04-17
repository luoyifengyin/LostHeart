using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.ObjectPool {
    public class ObjectPoolManager : Singleton<ObjectPoolManager> {
        private Dictionary<string, ObjectPool> m_ObjectPools = new Dictionary<string, ObjectPool>();
        private readonly GameObject m_Pool;

        public ObjectPoolManager() {
            m_Pool = GameObject.FindGameObjectWithTag("ObjectPool");
            if (!m_Pool) {
                m_Pool = new GameObject("ObjectPool") {
                    tag = "ObjectPool"
                };
                GameObject.DontDestroyOnLoad(m_Pool);
            }
        }

        public void AddCreateFunc(string key, Func<Object> func) {
            GetPool(key).createObject = func;
        }

        private ObjectPool GetPool(string key) {
            if (!m_ObjectPools.ContainsKey(key))
                m_ObjectPools.Add(key, m_Pool.AddComponent<ObjectPool>());
            return m_ObjectPools[key];
        }

        public TObject Get<TObject>(string key) where TObject : Object, new() {
            var pool = GetPool(key);
            var obj = pool.Get<TObject>();
            return obj;
        }

        public void Put(string key, Object obj) {
            var pool = GetPool(key);
            pool.Put(obj);
        }
    }
}
