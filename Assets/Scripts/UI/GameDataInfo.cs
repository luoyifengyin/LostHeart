using MyGameApplication.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI {
    public class GameDataInfo : MonoBehaviour {
        private static Text m_Text;
        private static List<string> m_Msgs = new List<string>();

        private void Awake() {
            m_Text = GetComponentInChildren<Text>();
        }

        public static void Display(object message) {
            m_Msgs.Add(message.ToString());
        }

        private void LateUpdate() {
            m_Text.text = string.Join("\n", m_Msgs);
            m_Msgs.Clear();
        }
    }
}
