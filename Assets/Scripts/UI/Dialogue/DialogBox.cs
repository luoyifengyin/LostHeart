using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class DialogBox : Dialogue {
        private Animator m_Animator;
        private readonly int OPEN_HASH = Animator.StringToHash("Open");
        private IEnumerator m_WaitOpen;
        private IEnumerator m_WaitClose;

        private Typewriter m_Typewriter;
        private Func<bool> m_DialogueControl;
        private WaitUntil m_Wait;

        private void Start() {
            m_Animator = GetComponentInChildren<Animator>();
            m_WaitOpen = new WaitUntil(() => {
                AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
                return info.IsName("Open");
            });
            m_WaitClose = new WaitUntil(() => {
                AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
                return info.IsName("Close");
            });

            m_Typewriter = new Typewriter(text);
            m_DialogueControl = () => CrossPlatformInputManager.GetButtonDown("Fire1")
                    || CrossPlatformInputManager.GetButtonDown("Submit");
            m_Wait = new WaitUntil(m_DialogueControl);
        }

        protected override IEnumerator OnShow() {
            text.text = "";
            m_Animator.SetBool(OPEN_HASH, true);
            yield return m_WaitOpen;
            yield return m_Typewriter.Typewrite(content);
        }

        protected override IEnumerator OnWait() {
            yield return null;
            yield return m_Wait;
        }

        protected override IEnumerator OnHide() {
            m_Animator.SetBool(OPEN_HASH, false);
            yield break;
        }

        private void Update() {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Open") && m_DialogueControl()) {
                m_Typewriter.DisplayImmediately();
            }
        }
    }
}
