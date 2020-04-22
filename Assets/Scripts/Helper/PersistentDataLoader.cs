using MyGameApplication.Item;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Helper {
    public class PersistentDataLoader : MonoBehaviour {
        [SerializeField] private GameObject persistentData = null;

        private void Awake() {
            if (!GameManager.Instance) Instantiate(persistentData);
        }
    }
}
