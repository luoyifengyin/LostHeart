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
            base.Awake();
            sceneController.onBeforeSceneUnload += Save;
        }

        private void OnEnable() {
            sceneController.onAfterSceneLoad += Load;
        }

        private void OnDisable() {
            sceneController.onAfterSceneLoad -= Load;
        }

        private void OnDestroy() {
            sceneController.onBeforeSceneUnload -= Save;
        }
    }
}
