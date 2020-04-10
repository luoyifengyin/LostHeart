#if UNITY_EDITOR
using MyGameApplication.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Manager.Debug {
    public class Test : MonoBehaviour {
        IEnumerator Start() {
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
    }
}
#endif
