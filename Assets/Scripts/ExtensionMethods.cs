using System;
using System.Collections;
using System.Collections.Generic;
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

        public static void Swap(this Array arr, int idxA, int idxB) {
            object c = arr.GetValue(idxA);
            arr.SetValue(arr.GetValue(idxB), idxA);
            arr.SetValue(c, idxB);
        }
        public static void Swap<T>(this List<T> list, int idxA, int idxB) {
            T c = list[idxA];
            list[idxA] = list[idxB];
            list[idxB] = c;
        }
    }
}
