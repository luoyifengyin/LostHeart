using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Manager {
    public class GameManager : MonoBehaviour {
        private void Awake() {
            ItemManager itemManager = ItemManager.Instance;
            DontDestroyOnLoad(gameObject);
        }
    }
}
