using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.ObjectPool {
    public class ObjectPool<T> : MonoBehaviour {
        private ConcurrentBag<T> m_UnuseCache = new ConcurrentBag<T>();
        public int capacity = -1;

        public Func<T> createObject;

        public void CreateSpecifiedObjectsById(int id, int cnt) {
            if (createObject == null) throw new Exception();
            for (int i = 0; i < cnt; i++) {
                m_UnuseCache.Add(createObject());
            }
        }

        public T Get() {
            if (!m_UnuseCache.IsEmpty && m_UnuseCache.TryTake(out T obj)) {
                if (obj is GameObject) {
                    ((GameObject)(object)obj).SetActive(true);
                }
            }
            else {
                if (createObject != null) obj = createObject();
                else obj = default;
            }
            return obj;
        }

        public void Put(T obj) {
            if (capacity < 0 || m_UnuseCache.Count < capacity) {
                if (obj is GameObject) {
                    var go = (GameObject)(object)obj;
                    go.transform.parent = transform;
                    go.SetActive(false);
                }
                m_UnuseCache.Add(obj);
            }
            else {
                if (obj is Object) {
                    Destroy((GameObject)(object)obj);
                }
            }
        }
    }
}
