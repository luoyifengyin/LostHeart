#if UNITY_EDITOR
using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using MyGameApplication.ObjectPool;
using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.Manager.TestTools {
    public class Test : MonoBehaviour {
        [ContextMenu("对话测试")]
        void DialogueTest() {
            Dialogue.DialogBox.StartCoroutine(Dialog());
        }
        private IEnumerator Dialog() {
            string content = "你好，世界！<color=red>123<color=blue>456</color>789</color>你好，世界！";
            //Dialogue.Subtitle.ShowDialogue(content);
            Dialogue.DialogBox.HideDialogue();
            yield return Dialogue.DialogBox.ShowDialogue(content);
            print("show " + Time.frameCount);
            yield return Dialogue.DialogBox.ShowDialogue("abcde");
            print("show " + Time.frameCount);
            yield return Dialogue.DialogBox.ShowDialogue("12345");
            print("show " + Time.frameCount);
            Dialogue.DialogBox.HideDialogue();
            yield return Dialogue.DialogBox.ShowDialogue(content);
            yield return Dialogue.DialogBox.ShowDialogue("abcde");
            yield return Dialogue.DialogBox.ShowDialogue("12345");
            //Dialogue.DialogBox.HideDialogue();
        }

        [ContextMenu("道具测试")]
        void ItemTest() {
            PlayerBag.Instance.AddItem(1);
            PlayerBag.Instance.AddItem(2);
            PlayerBag.Instance.AddItem(1);
        }

        [ContextMenu("对象池测试(GameObject)")]
        void ObjectPoolTest() {
            GameObject obj = new GameObject();
            ObjectPoolManager.Instance.Put("gameObject", obj);
            obj = new GameObject();
            ObjectPoolManager.Instance.Put("gameObject", obj);
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
        }

        [ContextMenu("生成指定数量的gameObject")]
        void CreatePoolGameObjects() {
            ObjectPoolManager.Instance.CreateSpecifiedObjects("gameObject", new GameObject(), 10);
        }

        [SerializeField] private GameObject m_Prefab;

        [ContextMenu("对象池测试(Prefab)")]
        void ObjectPoolPrefabTest() {
            ObjectPoolManager.Instance.SetCreateFunc("prefab", () => Instantiate(m_Prefab));
            ObjectPoolManager.Instance.Get<GameObject>("prefab");
            ObjectPoolManager.Instance.CreateSpecifiedObjects<GameObject>("prefab", 10);
        }

        [ContextMenu("对象池测试(自定义类)")]
        void ObjectPoolTest2() {
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

        [ContextMenu("协程测试")]
        void CoroutineTest() {
            TestObject obj = new TestObject();
            obj.testCoroutine();
        }
    }

    class TestObject {
        public int num = 1;
        public void testCoroutine() {
            Dialogue.DialogBox.StartCoroutine(test());
        }
        IEnumerator test() {
            for (int i = 0; i < 100; i++) {
                Debug.Log(i);
                yield return null;
            }
        }
    }

    public class MonoTest : MonoBehaviour {
        private void Start() {
            print("another test start");
        }
    }
}
#endif
