using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.ObjectPool {
    public class ObjectPool : MonoBehaviour {
        private ConcurrentBag<object> m_UnuseCache = new ConcurrentBag<object>();

        public Func<object> createObject;                   //对象的生成方法

        public int Capacity { get; set; } = -1;             //对象池容量
        public int Count { get => m_UnuseCache.Count; }     //池里剩下的对象数量

        private Object CloneObject(Object obj) {
            obj = Object.Instantiate(obj);
            if (obj is GameObject) {
                var go = obj as GameObject;
                go.transform.parent = gameObject.transform;
                go.SetActive(true);
            }
            return obj;
        }
        private T Create<T>() where T : class, new() {
            if (m_UnuseCache.TryPeek(out object obj) && obj is Object) {
                obj = CloneObject(obj as Object);
            }
            else if (createObject != null) obj = createObject();
            else obj = new T();
            return obj as T;
        }

        //生成指定数量的对象
        public void CreateSpecifiedObjects<T>(int cnt) where T : class, new() {
            for (int i = 0; i < cnt; i++) {
                Put(Create<T>());
            }
        }

        //从对象池中获取对象
        public T Get<T>() where T : class, new() {
            object obj;
            if (!m_UnuseCache.IsEmpty) {
                if (Count == 1 && m_UnuseCache.TryPeek(out obj) && obj is Object) {
                    obj = CloneObject(obj as Object);
                }
                else {
                    m_UnuseCache.TryTake(out obj);
                    if (obj is GameObject) {
                        (obj as GameObject).SetActive(true);
                    }
                }
            }
            else {
                obj = Create<T>();
            }
            return obj as T;
        }

        //把对象放回对象池
        public bool Put<T>(T obj) where T : class {
            if (Capacity < 0 || m_UnuseCache.Count < Capacity) {
                m_UnuseCache.Add(obj);
                if (obj is GameObject) {
                    var go = obj as GameObject;
                    go.transform.parent = gameObject.transform;
                    go.SetActive(false);
                }
                return true;
            }
            else if (obj is Object) {
                Object.Destroy(obj as Object);
            }
            return false;
        }
    }
}
