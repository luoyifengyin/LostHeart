using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using MyGameApplication.Manager;
using MyGameApplication.ObjectPool;
using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.Editor.TestTools {
    class Test : AbstractTest {
        [MenuItem("Test/对话测试/字幕", true)]
        [MenuItem("Test/对话测试/对话框", true)]
        [MenuItem("Test/道具测试", true)]
        [MenuItem("Test/协程测试", true)]
        static bool IsAbleToTest() {
            return EditorApplication.isPlaying;
        }

        [MenuItem("Test/对话测试/字幕", false, 0)]
        static void CaptionsTest() {
            CoroutineFactory.Start(Caption());
        }
        static private IEnumerator Caption() {
            yield return Dialogue.Subtitle.ShowDialogue(m_Content);
            yield return Dialogue.Subtitle.ShowDialogue("hello, world");
        }

        [MenuItem("Test/对话测试/对话框", false, 1)]
        static void DialogueTest() {
            CoroutineFactory.Start(Dialog());
        }
        static private readonly string m_Content = "你好，世界！<color=red>123<color=blue>456</color>789</color>你好，世界！<b>你好，世界！</b>你好，世界！<i>你好，世界！</i>你好，世界！<size=20>你好，世界！</size>你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！";
        static private IEnumerator Dialog() {
            Dialogue.DialogBox.HideDialogue();
            yield return Dialogue.DialogBox.ShowDialogue(m_Content);
            print("show " + Time.frameCount);
            yield return Dialogue.DialogBox.ShowDialogue("abcde");
            print("show " + Time.frameCount);
            yield return Dialogue.DialogBox.ShowDialogue("12345");
            print("show " + Time.frameCount);
            Dialogue.DialogBox.HideDialogue();
            yield return Dialogue.DialogBox.ShowDialogue(m_Content);
            yield return Dialogue.DialogBox.ShowDialogue("abcde");
            yield return Dialogue.DialogBox.ShowDialogue("12345");
            //Dialogue.DialogBox.HideDialogue();
        }

        [MenuItem("Test/道具测试", false, 10)]
        static void ItemTest() {
            PlayerBag.Instance.AddItem(1);
            PlayerBag.Instance.AddItem(2);
            PlayerBag.Instance.AddItem(1);
        }

        [MenuItem("Test/协程测试", false, 20)]
        static void CoroutineTest() {
            
        }
    }

    public class MonoTest : MonoBehaviour {
        private void Start() {
            print("another test start");
        }
    }
}
