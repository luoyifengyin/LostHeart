using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Data.Saver {
    public class TransformSaver : Saver {
        [SerializeField] private Transform transformToSave = null;
        public bool saveRotation = true;    //是否保存rotation
        public bool saveScale = false;      //是否保存scale

        protected string positionKey;
        protected string rotationKey;
        protected string scaleKey;

        protected override void Awake() {
            if (!transformToSave) transformToSave = transform;
            base.Awake();
            positionKey = key + "Position";
            rotationKey = key + "Rotation";
            scaleKey = key + "Scale";
        }

        protected override string CreateKey() {
            return SceneManager.GetActiveScene().name +
                transformToSave.name + transformToSave.GetType().FullName + uniqueIdentifier;
        }

        public override void Save() {
            gameData.Save(positionKey, transformToSave.position);
            if (saveRotation) gameData.Save(rotationKey, transformToSave.rotation);
            if (saveScale) gameData.Save(scaleKey, transformToSave.localScale);
        }

        public override void Load() {
            Vector3 position = default;
            if (gameData.Load(positionKey, ref position))
                transformToSave.position = position;
            Quaternion rotation = default;
            if (saveRotation && gameData.Load(rotationKey, ref rotation))
                transformToSave.rotation = rotation;
            Vector3 scale = default;
            if (saveScale && gameData.Load(scaleKey, ref scale))
                transformToSave.localScale = scale;
        }
    }
}
