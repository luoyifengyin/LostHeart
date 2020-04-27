using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Manager.TestTools {
    public class Developer : MonoBehaviour {
#if !UNITY_EDITOR
        private void Awake() {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
#endif
    }
}
