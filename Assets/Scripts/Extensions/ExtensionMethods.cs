using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication {
    public static class ExtensionMethods {
        public static GameObject GetGameObjectInParentWithTag(this Transform trans, string tag) {
            Transform cur = trans;
            while (!cur.gameObject.CompareTag(tag)) {
                cur = cur.parent;
                if (!cur) return null;
            }
            return cur.gameObject;
        }

        public static GameObject GetGameObjectInParentWithTag(this GameObject go, string tag) {
            return go.transform.GetGameObjectInParentWithTag(tag);
        }

        //public static void Swap(this Array arr, int idxA, int idxB) {
        //    object c = arr.GetValue(idxA);
        //    arr.SetValue(arr.GetValue(idxB), idxA);
        //    arr.SetValue(c, idxB);
        //}
        //public static void Swap<T>(this List<T> list, int idxA, int idxB) {
        //    T c = list[idxA];
        //    list[idxA] = list[idxB];
        //    list[idxB] = c;
        //}

        public static Transform[] GetChildren(this Transform transform) {
            var children = new Transform[transform.childCount];
            int i = 0;
            foreach(Transform child in transform) {
                children[i++] = child;
            }
            return children;
        }

        public static void BeforeSerialize<TKey, TValue>(this Dictionary<TKey, TValue> dic, List<TKey> keys, List<TValue> values) {
            keys.Clear();
            values.Clear();
            foreach(var kvp in dic) {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public static void AfterDeserialize<TKey, TValue>(this Dictionary<TKey, TValue> dic, List<TKey> keys, List<TValue> values) {
            dic.Clear();
            for (int i = 0; i != Math.Min(keys.Count, values.Count); i++)
                dic.Add(keys[i], values[i]);
        }
    }
}
