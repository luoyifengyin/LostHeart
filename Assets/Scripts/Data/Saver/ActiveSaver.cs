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

        private new void Awake() {
            if (activeToSave == gameObject) {
                Debug.LogError("ActiveSaver不能挂载在自己需要存储active信息的GameObject上，" +
                    "请挂载在另一个GameObject上并把activeToSave引用现在的GameObject。");
            }
            base.Awake();
        }

        public override void Save() {
            gameData.Save(key, activeToSave.activeSelf);
        }

        public override void Load() {
            bool active = default;
            if (gameData.Load(key, ref active))
                activeToSave.SetActive(active);
        }
    }
}
