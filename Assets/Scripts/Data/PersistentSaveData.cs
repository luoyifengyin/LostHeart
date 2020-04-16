using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    [CreateAssetMenu(menuName = "SaveData/PersistentSaveData")]
    public class PersistentSaveData : SaveData {
        private static readonly string SAVE_DATA_PATH =
            "Assets/ScriptableObjects/SaveData/PersistentSaveData.asset";

        public static PersistentSaveData Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void BeforeSceneLoad() {
            Instance = AssetDatabase.LoadAssetAtPath<PersistentSaveData>(SAVE_DATA_PATH);
        }
    }
}
