using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    public class TransformSaver : Saver {
        [SerializeField] private Transform transformToSave = null;
        public bool saveRotation = true;
        public bool saveScale = false;

        private string positionKey;
        private string rotationKey;
        private string scaleKey;

        protected override void Awake() {
            if (!transformToSave) transformToSave = transform;
            base.Awake();
            positionKey = key + "Position";
            rotationKey = key + "rotationKey";
            scaleKey = key + "scaleKey";
        }

        protected override string GetKey() {
            return transformToSave.name + transformToSave.GetType().FullName + uniqueIdentifier;
        }

        protected override void Save() {
            gameData.Save(positionKey, transformToSave.position);
            if (saveRotation) gameData.Save(rotationKey, transformToSave.rotation);
            if (saveScale) gameData.Save(scaleKey, transformToSave.localScale);
        }

        protected override void Load() {
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
