using MyGameApplication.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI {
    public class DialogChoices : MonoBehaviour {
        [SerializeField] private Animator m_Animator = null;

        private Button[] m_Buttons;
        private Text[] m_Texts;
        private int m_Result = -1;

        private void Awake() {
            m_Buttons = GetComponentsInChildren<Button>();
            m_Texts = GetComponentsInChildren<Text>();
        }

        public async Task<int> DisplayChoices(params string[] choices) {
            Animator dialogBoxAnimator = Dialogue.DialogBox.GetComponent<Animator>();
            if (dialogBoxAnimator.IsInTransition(0)) {
                dialogBoxAnimator.SetBool(AnimatorHash.OPEN, true);
            }
            RefreshUI(choices);
            m_Animator.SetBool(AnimatorHash.OPEN, true);

            m_Result = -1;
            await new WaitUntil(() => m_Result >= 0);
            m_Animator.SetBool(AnimatorHash.OPEN, false);
            if (dialogBoxAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == AnimatorHash.OPEN) {
                dialogBoxAnimator.SetBool(AnimatorHash.OPEN, false);
            }
            return m_Result;
        }

        private void RefreshUI(string[] choices) {
            int i;
            for (i = 0; i < choices.Length; i++) {
                m_Texts[i].text = choices[i];
                m_Buttons[i].gameObject.SetActive(true);
            }
            while(i < m_Buttons.Length) {
                m_Buttons[i++].gameObject.SetActive(false);
            }
        }

        public void OnChoose(int optionId) {
            m_Result = optionId;
        }
    }
}
