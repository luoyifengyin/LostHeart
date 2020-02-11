using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    [ExecuteInEditMode]
    public abstract class Saver : MonoBehaviour {
        private static string _saveDataPath = "Assets/ScriptableObjects/SaveData/PersistentSaveData.asset";
        [SerializeField] protected string uniqueIdentifier;
        public SaveData gameData;

        private SceneController sceneController;
        protected string key;

        protected virtual void Awake() {
            sceneController = SceneController.Instance;
            //if (!sceneController)
            //    throw new UnityException("SceneController could not be found!");
            if (!gameData) gameData = AssetDatabase.LoadAssetAtPath<SaveData>(_saveDataPath);
            key = GetKey();
        }

        private void OnEnable() {
            sceneController.onBeforeSceneUnload += Save;
            sceneController.onAfterSceneLoad += Load;
        }

        private void OnDisable() {
            sceneController.onBeforeSceneUnload -= Save;
            sceneController.onAfterSceneLoad -= Load;
        }

        protected abstract void Save();

        protected abstract void Load();

        protected virtual string GetKey() {
            return key = uniqueIdentifier;
        }
    }
}
