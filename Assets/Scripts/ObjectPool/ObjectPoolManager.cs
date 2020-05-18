using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace MyGameApplication.ObjectPool {
    //用键值对管理对象池，一个键对应一个对象池，可以根据需要存取的对象的不同而使用不同的key进行存取
    public class ObjectPoolManager : Singleton<ObjectPoolManager> {
        private Dictionary<string, ObjectPool> m_ObjectPools = new Dictionary<string, ObjectPool>();
        private readonly GameObject m_Pool;

        public ObjectPoolManager() {
            m_Pool = GameObject.FindGameObjectWithTag("ObjectPool");
            if (!m_Pool) {
                m_Pool = new GameObject("ObjectPools") {
                    tag = "ObjectPool"
                };
                GameObject.DontDestroyOnLoad(m_Pool);
            }
        }

        //根据键值获取对象池
        public ObjectPool GetPool(string key) {
            if (!m_ObjectPools.ContainsKey(key)) {
                GameObject goPool = new GameObject(key + " Pool");
                goPool.transform.parent = m_Pool.transform;
                m_ObjectPools.Add(key, goPool.AddComponent<ObjectPool>());
            }
            return m_ObjectPools[key];
        }

        public void SetCreateFunc(string key, Func<object> func) {
            GetPool(key).CreateObjectFunc = func;
        }

        //根据键值生成指定数量的相应对象，非GameObject对象使用这个方法
        public void CreateSpecifiedObjects<T>(string key, int cnt) where T : class, new() {
            GetPool(key).CreateSpecifiedObjects<T>(cnt);
        }
        //根据键值生成指定数量的相应的gameObject
        public void CreateSpecifiedObjects(string key, Object obj, int cnt) {
            GetPool(key).CreateSpecifiedObjects(obj, cnt);
        }

        public TObject Get<TObject>(string key) where TObject : class, new() {
            var obj = GetPool(key).Get<TObject>();
            return obj;
        }

        public void Put(string key, object obj) {
            GetPool(key).Put(obj);
        }

        public void DontPersistPool(string key) {
            var pool = GetPool(key).gameObject;
            pool.transform.parent = null;
            SceneManager.MoveGameObjectToScene(pool, SceneManager.GetActiveScene());
        }
    }
}
