using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class Dialogue {
        private static Subtitle m_Subtitle;

        private static DialogBox m_DialogBox;

        [Obsolete("Dialogue.main has been deprecated. Use Dialogue.Subtitle instead.")]
        public static Subtitle main {
            get {
                return m_Subtitle ?? (m_Subtitle = GameObject.FindObjectOfType<Subtitle>());
            }
        }

        public static Subtitle Subtitle {
            get {
                return m_Subtitle ?? (m_Subtitle = GameObject.FindObjectOfType<Subtitle>());
            }
        }

        public static DialogBox DialogBox {
            get {
                return m_DialogBox ?? (m_DialogBox = GameObject.FindObjectOfType<DialogBox>());
            }
        }

        [RuntimeInitializeOnLoadMethod]
        static void AfterSceneLoad() {
            _ = Subtitle; _ = DialogBox;
        }
    }
}
