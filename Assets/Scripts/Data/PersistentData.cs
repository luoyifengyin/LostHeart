using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    [CreateAssetMenu(menuName = "SaveData/PersistentSaveData")]
    public class PersistentData : SaveData {
        private static string _saveDataPath = "Assets/ScriptableObjects/SaveData/PersistentSaveData.asset";
        private static PersistentData _instance;

        public static PersistentData Instance {
            get {
                return _instance ?? (_instance = AssetDatabase.LoadAssetAtPath<PersistentData>(_saveDataPath));
            }
        }
    }
}
