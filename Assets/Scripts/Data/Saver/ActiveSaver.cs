using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Data.Saver {
    public class ActiveSaver : Saver {
        [SerializeField] private GameObject activeToSave = null;

        protected override string CreateKey() {
            return SceneManager.GetActiveScene().name +
                activeToSave.name + "Active" + uniqueIdentifier;
        }

        public override void Save() {
            gameData.Save(key, activeToSave.activeSelf);
        }

        public override void Load() {
            bool active = default;
            if (gameData.Load(key, ref active))
                activeToSave.SetActive(active);
        }

        private new void Awake() {
            if (!activeToSave) activeToSave = gameObject;
            base.Awake();

#if UNITY_EDITOR
            if (!sceneController) return;
#endif
            if (AutoSaveOnSwitchScene && activeToSave == gameObject) {
                sceneController.onBeforeSceneUnload += Save;
            }
        }

        private new void OnEnable() {
#if UNITY_EDITOR
            if (!sceneController) return;
#endif
            if (AutoSaveOnSwitchScene) {
                sceneController.onAfterSceneLoad += Load;
                if (activeToSave != gameObject) {
                    sceneController.onAfterSceneLoad += Save;
                }
            }
        }

        private new void OnDisable() {
#if UNITY_EDITOR
            if (!sceneController) return;
#endif
            if (AutoSaveOnSwitchScene) {
                sceneController.onAfterSceneLoad -= Load;
                if (activeToSave != gameObject) {
                    sceneController.onAfterSceneLoad -= Save;
                }
            }
        }

        private void OnDestroy() {
#if UNITY_EDITOR
            if (!sceneController) return;
#endif
            if (AutoSaveOnSwitchScene && activeToSave == gameObject) {
                sceneController.onBeforeSceneUnload -= Save;
            }
        }
    }
}
