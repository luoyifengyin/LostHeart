using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class DialogBox : Typewriter {
        private Animator m_Animator;
        private readonly int OPEN_HASH = Animator.StringToHash("Open");
        private IEnumerator m_WaitOpen;
        private IEnumerator m_WaitClose;


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

            m_DialogueControl = () => CrossPlatformInputManager.GetButtonDown("Fire1")
                    || CrossPlatformInputManager.GetButtonDown("Submit");
            m_Wait = new WaitUntil(m_DialogueControl);
        }

        protected override IEnumerator OnShow() {
            //print("dialogue on shown");
            //if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Close")) {
                text.text = "";
                m_Animator.SetBool(OPEN_HASH, true);
                yield return m_WaitOpen;
            //}
        }

        protected override IEnumerator OnWait() {
            yield return null;
            yield return m_Wait;
        }

        protected override IEnumerator OnHide() {
            yield break;
        }

        public override Coroutine HideDialogue() {
            //print("hide dialogue");
            m_Animator.SetBool(OPEN_HASH, false);
            return StartCoroutine(m_WaitClose);
        }

        private void Update() {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Open") && m_DialogueControl())
                DisplayImmediately();
        }
    }
}
