using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Data {
    [ExecuteInEditMode]
    public abstract class Saver : MonoBehaviour {
        [SerializeField] protected string uniqueIdentifier;
        [SerializeField] protected SaveData gameData;
        [SerializeField] private bool autoSaveOnSwitchScene = true;

        private SceneController sceneController;
        protected string key;

        public bool AutoSaveOnSwitchScene { get { return autoSaveOnSwitchScene; } }

        protected virtual void Awake() {
            sceneController = SceneController.Instance;
            //if (!sceneController)
            //    throw new UnityException("SceneController could not be found!");
            if (!gameData) gameData = PersistentData.Instance;
            key = CreateKey();
        }

        private void OnEnable() {
            if (autoSaveOnSwitchScene) {
                sceneController.onBeforeSceneUnload += Save;
                sceneController.onAfterSceneLoad += Load;
            }
        }

        private void OnDisable() {
            sceneController.onBeforeSceneUnload -= Save;
            sceneController.onAfterSceneLoad -= Load;
        }

        public abstract void Save();

        public abstract void Load();

        protected virtual string CreateKey() {
            return SceneManager.GetActiveScene().name + gameObject.name + uniqueIdentifier;
        }
    }
}
