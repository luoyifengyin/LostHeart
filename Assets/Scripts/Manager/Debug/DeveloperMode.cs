#define __DEBUG__
using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Manager.Debug {
    public class DeveloperMode : MonoBehaviour {
#if !UNITY_EDITOR
        private void Awake() {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
#endif

#if UNITY_EDITOR && __DEBUG__
        private string[] sceneNames = { "CarRacing", "Second", "Maze" };
        private int curSceneIndex = -1;
        private void OnGUI() {
            if (GUI.Button(new Rect(0, 0, 100, 50), "Switch Scene")) {
                if (curSceneIndex == -1) {
                    curSceneIndex = Array.FindIndex(sceneNames,
                        str => str == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                }
                curSceneIndex = (curSceneIndex + 1) % sceneNames.Length;
                SceneController.LoadScene(sceneNames[curSceneIndex]);
            }
            else if (GUI.Button(new Rect(0, 50, 100, 50), "Save Game")) {
                GameManager.Instance.SaveGame();
            }
        }
#endif
    }
}
