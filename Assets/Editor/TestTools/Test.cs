using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using MyGameApplication.Manager;
using MyGameApplication.ObjectPool;
using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyGameApplication.TestTools {
    class Test : AbstractTest {
        [MenuItem("Test/对话测试/字幕", true)]
        [MenuItem("Test/对话测试/对话框", true)]
        [MenuItem("Test/道具测试/增加道具", true)]
        [MenuItem("Test/道具测试/减少道具", true)]
        [MenuItem("Test/协程测试", true)]
        static bool IsAbleToTest() {
            return EditorApplication.isPlaying;
        }

        [MenuItem("Test/对话测试/字幕", false, 0)]
        static void CaptionsTest() {
            CoroutineFactory.Start(Caption());
        }
        static private IEnumerator Caption() {
            yield return Dialogue.Caption.ShowDialogue(m_Content);
            yield return Dialogue.Caption.ShowDialogue("hello, world");
        }

        [MenuItem("Test/对话测试/对话框", false, 1)]
        static void DialogueTest() {
            Dialog();
        }
        static private readonly string m_Content = "你好，世界！<color=red>123<color=blue>456</color>789</color>你好，世界！<b>你好，世界！</b>你好，世界！<i>你好，世界！</i>你好，世界！<size=20>你好，世界！</size>你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！你好，世界！";
        static private async void Dialog() {
            //Dialogue.DialogBox.HideDialogue();
            await Dialogue.DialogBox.ShowDialogue(m_Content);
            //print(await ienumeratorTest());
            //print("show " + Time.frameCount);
            await Dialogue.DialogBox.ShowDialogue("abcde");
            //print("show " + Time.frameCount);
            await Dialogue.DialogBox.ShowDialogue("12345");
            //print("show " + Time.frameCount);
            //Dialogue.DialogBox.HideDialogue();
            await Dialogue.DialogBox.ShowDialogue(m_Content);
            DialogChoices choices = Dialogue.DialogChoices;
            int sel = await choices.DisplayChoices("A", "123");
            print("choose: " + sel);
            await Dialogue.DialogBox.ShowDialogue("abcde2346377514ssdfhgjdfjvhdz");
            await Dialogue.DialogBox.ShowDialogue("12345");
            //Dialogue.DialogBox.HideDialogue();
        }

        [MenuItem("Test/道具测试/增加道具", false, 10)]
        static void ItemAddTest() {
            PlayerBag.Instance.AddItem(1);
            PlayerBag.Instance.AddItem(2);
            PlayerBag.Instance.AddItem(1);
        }
        [MenuItem("Test/道具测试/减少道具", false, 11)]
        static void ItemReduceTest() {
            PlayerBag.Instance.AddProp(1, -2);
            PlayerBag.Instance.AddProp(2, -1);
        }

        [MenuItem("Test/协程测试", false, 20)]
        static async void CoroutineTest() {
            await new WaitForSeconds(1);
            string str = await ienumeratorTest() as string;
            print(str);
            //await Dialogue.DialogBox.ShowDialogue(m_Content);
            int sel = await Dialogue.DialogChoices.DisplayChoices("A", "123");
            print(sel);
        }

        static IEnumerator ienumeratorTest() {
            yield return CoroutineFactory.Start(aaa());
            yield return "1234567";
        }
        static IEnumerator aaa() {
            for(int i = 0;i < 100; i++) {
                print("aaa " + i);
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
