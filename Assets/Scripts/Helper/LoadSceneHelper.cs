using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGameApplication.Helper {
    public class LoadSceneHelper : MonoBehaviour {
        public GameObject panel;

        private void Start() {
            panel.SetActive(false);
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.P)) {
                panel.SetActive(!panel.activeSelf);
            }
        }

        public void LoadToMainMenu() {
            SceneController.LoadScene("MainMenu");
        }

        public void LoadToOrigin() {
            SceneController.LoadScene("origin");
        }

        public void LoadToCarRacing() {
            SceneController.LoadScene("CarRacing");
        }

        public void LoadToSecond() {
            SceneController.LoadScene("Second");
        }

        public void LoadToMaze() {
            SceneController.LoadScene("Maze");
        }

        public void LoadToNormalEnd() {
            SceneController.LoadScene("NormalEnd");
        }

        public void LoadToGoodEnd() {
            SceneController.LoadScene("GoodEnd");
        }

        public void LoadToBadEnd() {
            SceneController.LoadScene("BadEnd");
        }

        public void LoadToBetMan() {
            SceneController.LoadScene("BetMan");
        }

        public void LoadToFireworksWar() {
            SceneController.LoadScene("GameScene");
        }
    }
}
