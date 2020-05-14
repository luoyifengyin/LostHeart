using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.ObjectPool {
    public class ObjectPool : MonoBehaviour {
        private ConcurrentBag<object> m_UnuseCache = new ConcurrentBag<object>();

        private Func<object> m_CreateObjectFunc;
        public Func<object> CreateObjectFunc {
            private get => m_CreateObjectFunc;
            set => m_CreateObjectFunc = () => {
                object obj = value();
                if (obj is GameObject) {
                    GameObject go = obj as GameObject;
                    if (!go.transform.parent) go.transform.parent = transform;
                }
                return obj;
            };
        }

        public int Capacity { get; set; } = -1;         //对象池容量（-1代表无上限）
        public int Count => m_UnuseCache.Count;         //池里剩下的对象数量

        private Object CloneObject(Object obj) {
            Object newObj = Object.Instantiate(obj, transform);
            newObj.name = obj.name;
            return newObj;
        }
        private T Create<T>() where T : class, new() {
            object obj;
            if (CreateObjectFunc != null) obj = CreateObjectFunc();
            else if (m_UnuseCache.TryPeek(out obj) && obj is Object) {
                obj = CloneObject(obj as Object);
            }
            else obj = new T();
            return obj as T;
        }

        //生成指定数量的对象
        public void CreateSpecifiedObjects<T>(int cnt) where T : class, new() {
            for (int i = 0; i < cnt; i++) {
                Put(Create<T>());
            }
        }
        public void CreateSpecifiedObjects(Object obj, int cnt) {
            if (cnt <= 0) return;
            Put(obj);
            CreateSpecifiedObjects<Object>(cnt - 1);
        }

        //从对象池中获取对象
        public T Get<T>() where T : class, new() {
            object obj;
            if (!m_UnuseCache.IsEmpty) {
                if (CreateObjectFunc == null && Count == 1 && m_UnuseCache.TryPeek(out obj) && obj is Object) {
                    obj = CloneObject(obj as Object);
                }
                else {
                    m_UnuseCache.TryTake(out obj);
                }
            }
            else {
                obj = Create<T>();
            }
            if (obj is GameObject) {
                (obj as GameObject).SetActive(true);
            }
            return obj as T;
        }

        //把对象放回对象池
        public bool Put<T>(T obj) where T : class {
            if (Capacity < 0 || m_UnuseCache.Count < Capacity) {
                m_UnuseCache.Add(obj);
                if (obj is GameObject) {
                    var go = obj as GameObject;
                    if (!go.transform.parent) go.transform.parent = transform;
                    go.SetActive(false);
                }
                return true;
            }
            else if (obj is Object) {
                Object.Destroy(obj as Object);
            }
            return false;
        }

        public void Remove(int cnt) {
            for(int i = 0;i < cnt; i++) {
                if (m_UnuseCache.TryTake(out object obj)) {
                    if (obj is Object) Destroy(obj as Object);
                }
                else break;
            }
        }
        public void Clear() {
            Remove(Count);
        }
    }
}
