using MyGameApplication.Item;
using MyGameApplication.Manager;
using MyGameApplication.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication {
    public class PersistentDataLoader : MonoBehaviour {
        [SerializeField] private GameObject persistentData = null;

        private void Awake() {
            if (!FindObjectOfType<GameManager>())
                Instantiate(persistentData);

            //Test();
        }

        private void Test() {
            PlayerBag.Instance.AddItem(1);
            PlayerBag.Instance.AddItem(2);
        }
    }
}
