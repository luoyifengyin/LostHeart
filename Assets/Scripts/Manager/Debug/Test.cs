#if UNITY_EDITOR
using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using MyGameApplication.ObjectPool;
using MyGameApplication.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Manager.Debug {
    public class Test : MonoBehaviour {
        IEnumerator Start() {
            test();
            ObjectPoolTest();
            ObjectPoolTest2();

            //yield return new WaitForSeconds(1);
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
            Object obj = new Object();
            ObjectPoolManager.Instance.Put("test", obj);
            obj = new Object();
            ObjectPoolManager.Instance.Put("test", obj);
            ObjectPoolManager.Instance.Get<Object>("test");
            ObjectPoolManager.Instance.Get<Object>("test");
            ObjectPoolManager.Instance.Get<Object>("test");
        }
    }
}
#endif
