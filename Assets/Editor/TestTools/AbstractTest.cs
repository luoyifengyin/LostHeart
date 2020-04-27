using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.TestTools {
    abstract class AbstractTest {
        private static readonly string TEST_ASSET_ROOT_PATH = "Assets/Prefabs/Test/";

        protected static T Load<T>(string assetPath) where T : Object {
            return AssetDatabase.LoadAssetAtPath<T>(TEST_ASSET_ROOT_PATH + assetPath);
        }

        [MenuItem("Test", true)]
        private static bool IsAbleToTest() {
            return EditorApplication.isPlaying;
        }

        protected static void print(object message) {
            Debug.Log(message);
        }
    }
}
