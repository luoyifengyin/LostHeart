using MyGameApplication.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.Editor.TestTools {
    class ObjectPoolTest : AbstractTest {
        static GameObject m_Prefab;

        static ObjectPoolTest() {
            m_Prefab = Load<GameObject>("TestPrefab.prefab");
        }

        [MenuItem("Test/对象池测试(GameObject)", true)]
        [MenuItem("Test/对象池测试，生成指定数量的gameObject", true)]
        [MenuItem("Test/对象池测试(Prefab)", true)]
        [MenuItem("Test/对象池测试(自定义类)", true)]
        static bool isAbleToTest() {
            return EditorApplication.isPlaying;
        }
        
        [MenuItem("Test/对象池测试(GameObject)", false, 1000)]
        static void GameObjectTest() {
            GameObject obj = new GameObject();
            ObjectPoolManager.Instance.Put("gameObject", obj);
            obj = new GameObject();
            ObjectPoolManager.Instance.Put("gameObject", obj);
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
        }

        [MenuItem("Test/对象池测试，生成指定数量的gameObject", false, 1010)]
        static void CreatePoolGameObjects() {
            ObjectPoolManager.Instance.CreateSpecifiedObjects("gameObject", new GameObject(), 10);
        }

        [MenuItem("Test/对象池测试(Prefab)", false, 1020)]
        static void ObjectPoolPrefabTest() {
            ObjectPoolManager.Instance.SetCreateFunc("prefab", () => Object.Instantiate(m_Prefab));
            //ObjectPoolManager.Instance.Get<GameObject>("prefab");
            ObjectPoolManager.Instance.CreateSpecifiedObjects<GameObject>("prefab", 10);
        }

        [MenuItem("Test/对象池测试(自定义类)", false, 1030)]
        static void test() {
            TestObject obj = new TestObject();
            if (obj != null) print("obj not null");
            else print("obj is null");
            obj.num = 10;
            print(obj.num);
            ObjectPoolManager.Instance.Put("test", obj);

            obj = new TestObject {
                num = 11
            };
            print(obj.num);
            ObjectPoolManager.Instance.Put("test", obj);
            //ObjectPoolManager.Instance.Put("test", 1);

            obj = ObjectPoolManager.Instance.Get<TestObject>("test");
            print(obj.num);
            print(ObjectPoolManager.Instance.Get<TestObject>("test").num);
            print(ObjectPoolManager.Instance.Get<TestObject>("test").num);
        }
    }
}
