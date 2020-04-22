using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Data.Saver {
    [ExecuteAlways]
    public abstract class Saver : MonoBehaviour {
        [SerializeField] protected string uniqueIdentifier;
        [SerializeField] protected SaveData gameData;
        [SerializeField] private bool autoSaveOnSwitchScene = true; //是否在切换场景时自动保存（仅在游戏运行期间保存，不是保存在磁盘上(存档)）

        protected SceneController sceneController;
        protected string key;

        public bool AutoSaveOnSwitchScene => autoSaveOnSwitchScene;

        protected void Awake() {
            sceneController = SceneController.Instance;
            //if (!sceneController)
            //    throw new UnityException("SceneController could not be found!");
            if (!gameData) gameData = PersistentSaveData.Instance;
            key = CreateKey();
        }

        protected void OnEnable() {
#if UNITY_EDITOR
            if (!sceneController) return;
#endif
            if (autoSaveOnSwitchScene) {
                sceneController.onBeforeSceneUnload += Save;
                sceneController.onAfterSceneLoad += Load;
            }
        }

        protected void OnDisable() {
#if UNITY_EDITOR
            if (!sceneController) return;
#endif
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
