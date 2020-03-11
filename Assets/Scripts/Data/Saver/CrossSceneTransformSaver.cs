using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Data.Saver {
    //保存持久化的gameObject的transform信息
    public class CrossSceneTransformSaver : TransformSaver {
        public override void Save() {
            //CreateKey();
            base.Save();
        }

        public override void Load() {
            CreateKey();
            base.Load();
        }

        protected override string CreateKey() {
            key = base.CreateKey();
            positionKey = key + "Position";
            rotationKey = key + "Rotation";
            scaleKey = key + "Scale";
            return key;
        }
    }
}
