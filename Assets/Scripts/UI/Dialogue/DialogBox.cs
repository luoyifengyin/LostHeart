using MyGameApplication.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class DialogBox : Dialogue {
        private Animator m_Animator;
        private IEnumerator m_WaitOpen;

        private Typewriter m_Typewriter;
        private WaitUntil m_Wait;

        private void Start() {
            m_Animator = GetComponentInChildren<Animator>();
            m_Typewriter = new Typewriter(text);

            //m_WaitOpen = new WaitUntil(() => {
            //    AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
            //    return info.IsName("Open");
            //});
            m_WaitOpen = m_Animator.CreateWaitTransition();
            m_Wait = new WaitUntil(DialogueControl);
        }

        private bool DialogueControl() {
            return Input.GetMouseButtonDown(0) || CrossPlatformInputManager.GetButtonDown("Submit");
        }

        protected override IEnumerator OnShow() {
            text.text = "";
            m_Animator.SetBool(AnimatorHash.OPEN, true);
            yield return m_WaitOpen;
            yield return m_Typewriter.Typewrite(content);
        }

        protected override IEnumerator OnWait() {
            yield return null;
            yield return m_Wait;
        }

        protected override IEnumerator OnHide() {
            m_Animator.SetBool(AnimatorHash.OPEN, false);
            yield break;
        }

        private void Update() {
            if (m_Typewriter.IsTypewriting && DialogueControl()) {
                m_Typewriter.DisplayImmediately();
            }
        }
    }
}
