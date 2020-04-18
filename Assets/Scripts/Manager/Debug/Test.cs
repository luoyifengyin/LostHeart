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

namespace MyGameApplication.Manager.Debug {
    public class Test : MonoBehaviour {
        IEnumerator Start() {
            test();
            ObjectPoolTest();
            ObjectPoolTest2();

            yield return new WaitForSeconds(1);
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

        void test() {
            PlayerBag.Instance.AddItem(1);
            PlayerBag.Instance.AddItem(2);
            PlayerBag.Instance.AddItem(1);

            gameObject.AddComponent<MonoTest>();
        }

        void ObjectPoolTest() {
            GameObject obj = new GameObject();
            ObjectPoolManager.Instance.Put("gameObject", obj);
            obj = new GameObject();
            ObjectPoolManager.Instance.Put("gameObject", obj);
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
            ObjectPoolManager.Instance.Get<GameObject>("gameObject");
        }


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
    }

    public class TestObject {
        public int num = 1;
        public TestObject() {
            num = 1;
        }
    }

    public class MonoTest : MonoBehaviour {
        private void Start() {
            print("another test start");
        }
    }
}
#endif
