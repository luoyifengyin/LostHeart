using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Data.Saver {
    public abstract class Saver : MonoBehaviour, ISaver {
        [SerializeField] protected string uniqueIdentifier;
        protected SaveData gameData;
        [SerializeField] private bool autoSaveOnSwitchScene = true; //是否在切换场景时自动保存（仅在游戏运行期间保存，不是保存在磁盘上(存档)）

        protected SceneController sceneController;
        protected string key;

        public bool AutoSaveOnSwitchScene => autoSaveOnSwitchScene;

        protected void Awake() {
            sceneController = SceneController.Instance;
            //if (!sceneController)
            //    throw new UnityException("SceneController could not be found!");
            gameData = GameManager.Instance.GameData;
            key = CreateKey();
        }

        private void OnEnable() {
            if (autoSaveOnSwitchScene) {
                sceneController.onBeforeSceneUnload += Save;
                sceneController.onAfterSceneLoad += Load;
            }
        }

        private void OnDisable() {
            if (autoSaveOnSwitchScene) {
                sceneController.onBeforeSceneUnload -= Save;
                sceneController.onAfterSceneLoad -= Load;
            }
        }

        public abstract void Save();

        public abstract void Load();

        protected virtual string CreateKey() {
            return SceneManager.GetActiveScene().name + gameObject.name + uniqueIdentifier;
        }
    }
}
