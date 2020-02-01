using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication {
    public class Dialogue : MonoBehaviour {
        private Text m_Text;
        [SerializeField] private float m_InputIntervalTime = 0.1f;
        [SerializeField] private float m_FadeOutSpeed = 1f;
        private string m_Words;
        private Coroutine m_Coroutine;

        private void SetAlpha(float alpha) {
            Color color = m_Text.color;
            m_Text.color = new Color(color.r, color.b, color.g, alpha);
        }

        private void Awake() {
            m_Text = GetComponent<Text>();
        }

        // Start is called before the first frame update
        void Start() {
            SetAlpha(0);
            ShowDialogue("你好，世界。你好，世界。你好，世界。你好，世界。");
        }

        private IEnumerator FadeIn(float intervalTime) {
            SetAlpha(1f);
            int curPos = 0;
            while (true) {
                m_Text.text = m_Words.Substring(0, ++curPos);
                if (curPos >= m_Words.Length) {
                    Invoke("HideDialogue", 3);
                    m_Coroutine = null;
                    yield break;
                }
                yield return new WaitForSeconds(intervalTime);
            }
        }

        private IEnumerator FadeOut() {
            while (true) {
                float alpha = m_Text.color.a - m_FadeOutSpeed * Time.deltaTime;
                SetAlpha(Mathf.Clamp(alpha, 0, 1f));
                if (alpha <= 0) {
                    m_Coroutine = null;
                    yield break;
                }
                yield return null;
            }
        }

        public void ShowDialogue(string content, Color? color = null, float? intervalTime = null) {
            m_Text.color = color ?? Color.white;
            if (m_Coroutine != null) {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
            m_Words = content;
            if (intervalTime == 0) m_Text.text = m_Words;
            else m_Coroutine = StartCoroutine(FadeIn(intervalTime ?? m_InputIntervalTime));
        }
        private void HideDialogue() {
            m_Coroutine = StartCoroutine(FadeOut());
        }
    }
}
