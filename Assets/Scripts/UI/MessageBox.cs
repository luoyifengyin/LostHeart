using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class MessageBox : MonoBehaviour {
        public static MessageBox Instance { get; private set; }

        [SerializeField] private Text m_Text;

        private Animator m_Animator;
        private readonly int HASH_OPEN = Animator.StringToHash("Open");

        private WaitUntil m_Wait;
        private CustomYieldInstruction m_WaitTransition;

        private void Awake() {
            Instance = this;

            if (!m_Text) m_Text = GetComponent<Text>();
            m_Animator = GetComponent<Animator>();

            m_Wait = new WaitUntil(() => CrossPlatformInputManager.GetButtonDown("Submit")
                    || Input.GetMouseButtonDown(0));

            m_WaitTransition = new WaitWhile(() => {
                return m_Animator.IsInTransition(0);
            });
        }

        public Coroutine Show(string message) {
            StopAllCoroutines();
            m_Text.text = message;
            return StartCoroutine(Wait());
        }

        private IEnumerator Wait() {
            m_Animator.SetBool(HASH_OPEN, true);
            yield return m_WaitTransition;

            yield return m_Wait;

            m_Animator.SetBool(HASH_OPEN, false);
            yield return m_WaitTransition;
        }
    }
}
