using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGameApplication.UI {
    public class App : MonoBehaviour {
        void Start() {
            GetComponent<Button>().onClick.AddListener(() => {
                SceneController.LoadScene("CarRacing");
            });
        }
    }
}
