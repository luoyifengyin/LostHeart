using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    [CreateAssetMenu(menuName = "SaveData/PersistentSaveData")]
    public class PersistentSaveData : SaveData {
        private static readonly string SAVE_DATA_PATH = "Assets/ScriptableObjects/SaveData/PersistentSaveData.asset";
        private static PersistentSaveData _instance;

        public static PersistentSaveData Instance {
            get {
                return _instance ?? (_instance = AssetDatabase.LoadAssetAtPath<PersistentSaveData>(SAVE_DATA_PATH));
            }
        }
    }
}
