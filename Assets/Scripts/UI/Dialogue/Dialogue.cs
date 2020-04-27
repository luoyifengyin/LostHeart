using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public abstract class Dialogue : MonoBehaviour {
        public Text text;
        protected string content;
        private Coroutine m_Coroutine;

        private void Awake() {
            if (!text) text = GetComponentInChildren<Text>();
        }

        public Coroutine ShowDialogue(string content, Color? color = null) {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(content = content.Trim())) return null;
            text.color = color ?? Color.white;
            if (m_Coroutine != null) {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
            this.content = content;
            return m_Coroutine = StartCoroutine(Display());
        }

        private IEnumerator Display() {
            yield return OnShow();
            yield return OnWait();
            yield return OnHide();
            m_Coroutine = null;
        }

        public virtual Coroutine HideDialogue() {
            if (m_Coroutine != null) StopCoroutine(m_Coroutine);
            return m_Coroutine = StartCoroutine(OnHide());
        }

        protected abstract IEnumerator OnShow();

        protected abstract IEnumerator OnWait();

        protected abstract IEnumerator OnHide();


        private static Caption s_Caption;

        private static DialogBox s_DialogBox;

        [Obsolete("Dialogue.main has been deprecated. Use Dialogue.Subtitle instead.")]
        public static Caption main {
            get {
                return s_Caption ?? (s_Caption = GameObject.FindObjectOfType<Caption>());
            }
        }

        public static Caption Caption {
            get {
                return s_Caption ?? (s_Caption = GameObject.FindObjectOfType<Caption>());
            }
        }

        public static DialogBox DialogBox {
            get {
                return s_DialogBox ?? (s_DialogBox = GameObject.FindObjectOfType<DialogBox>());
            }
        }

        [RuntimeInitializeOnLoadMethod]
        static void AfterSceneLoad() {
            _ = Caption; _ = DialogBox;
        }
    }
}
