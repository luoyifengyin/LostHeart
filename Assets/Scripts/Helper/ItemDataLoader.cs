using MyGameApplication.Item;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Helper {
    public class ItemDataLoader : MonoBehaviour {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void LoadItemData() {
            _ = ItemManager.Instance;
        }

        //private void Start() {
        //    LoadItemData();
        //}
    }
}
