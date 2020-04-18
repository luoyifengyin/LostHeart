using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.ObjectPool {
    //用键值对管理对象池，一个键对应一个对象池，可以根据需要存取的对象的不同而使用不同的key进行存取
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

        //根据键值获取对象池
        public ObjectPool GetPool(string key) {
            if (!m_ObjectPools.ContainsKey(key))
                m_ObjectPools.Add(key, m_Pool.AddComponent<ObjectPool>());
            return m_ObjectPools[key];
        }

        public void SetCreateFunc(string key, Func<object> func) {
            GetPool(key).createObject = func;
        }

        //根据键值生成指定数量的相应对象
        public void CreateSpecifiedObjects<T>(string key, int cnt) where T : class, new() {
            GetPool(key).CreateSpecifiedObjects<T>(cnt);
        }
        public void CreateSpecifiedObjects(string key, Object obj, int cnt) {
            if (cnt <= 0) return;
            var pool = GetPool(key);
            pool.Put(obj);
            pool.CreateSpecifiedObjects<Object>(cnt - 1);
        }

        public TObject Get<TObject>(string key) where TObject : class, new() {
            var obj = GetPool(key).Get<TObject>();
            return obj;
        }

        public void Put(string key, object obj) {
            GetPool(key).Put(obj);
        }
    }
}
