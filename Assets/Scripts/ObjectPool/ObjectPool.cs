using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.ObjectPool {
    public class ObjectPool : MonoBehaviour {
        private ConcurrentBag<Object> m_UnuseCache = new ConcurrentBag<Object>();
        public Func<Object> createObject;

        private bool m_IsGameObject = false;

        public int Capacity { get; set; } = -1;

        public int Count { get => m_UnuseCache.Count; }

        private T Create<T>() where T : Object, new() {
            T obj;
            if (createObject != null) obj = createObject() as T;
            else obj = new T();
            return obj;
        }

        public void CreateSpecifiedObjects<T>(int cnt) where T : Object, new() {
            for (int i = 0; i < cnt; i++) {
                Object obj = Create<T>();
                if (obj == null) throw new Exception();
                m_UnuseCache.Add(obj);
            }
        }

        public T Get<T>() where T : Object, new() {
            Object obj;
            if (!m_UnuseCache.IsEmpty) {
                if (Count == 1 && m_IsGameObject){
                    m_UnuseCache.TryPeek(out obj);
                    obj = Object.Instantiate(obj);
                    if (obj is GameObject) {
                        var go = obj as GameObject;
                        go.transform.parent = gameObject.transform;
                        go.SetActive(true);
                    }
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

        public bool Put(Object obj) {
            if (Capacity < 0 || m_UnuseCache.Count < Capacity) {
                m_UnuseCache.Add(obj);
                if (obj is GameObject) {
                    var go = obj as GameObject;
                    go.transform.parent = gameObject.transform;
                    go.SetActive(false);
                    m_IsGameObject = true;
                }
                return true;
            }
            else {
                Object.Destroy(obj);
            }
            return false;
        }
    }
}
