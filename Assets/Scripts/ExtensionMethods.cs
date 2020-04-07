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
    }
}
