using MyGameApplication.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Maze {
    public class NumberInputPanel : MonoBehaviour {
        public static NumberInputPanel Instance { get; private set; }
        private Animator m_Animator;

        [SerializeField] private Text m_Text;
        [SerializeField] private Transform m_NumberPad;
        private StringBuilder m_StringBuilder = new StringBuilder();

        [SerializeField] private Button m_ComfirmBtn;
        [SerializeField] private Button m_CancelBtn;
        [SerializeField] private Button m_DelBtn;
        private sbyte m_Finish = 0;

        private void Awake() {
            Instance = this;
            m_Animator = GetComponent<Animator>();
            var buttons = m_NumberPad.GetComponentsInChildren<Button>();

            for(int i = 0;i < 12;i++) {
                var btn = buttons[i];
                btn.onClick.AddListener(() => {
                    m_StringBuilder.Append(btn.GetComponentInChildren<Text>().text);
                    m_Text.text = m_StringBuilder.ToString();
                });
            }

            m_DelBtn.onClick.AddListener(() => {
                m_StringBuilder.Remove(m_StringBuilder.Length - 1, 1);
                m_Text.text = m_StringBuilder.ToString();
            });
            m_ComfirmBtn.onClick.AddListener(() => m_Finish = 1);
            m_CancelBtn.onClick.AddListener(() => m_Finish = -1);
        }

        public async Task<string> Show() {
            m_Text.text = "";
            m_Animator.SetBool(AnimatorHash.OPEN, true);

            m_Finish = 0;
            await new WaitWhile(() => {
                if (CrossPlatformInputManager.GetButtonDown("Submit")) m_Finish = 1;
                else if (CrossPlatformInputManager.GetButtonDown("Cancel")) m_Finish = -1;
                return m_Finish == 0;
            });

            m_Animator.SetBool(AnimatorHash.OPEN, false);
            if (m_Finish > 0) return m_Text.text;
            else return null;
        }
    }
}
