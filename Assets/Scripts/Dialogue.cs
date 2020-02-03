using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication {
    public class Dialogue : MonoBehaviour {
        private Text m_Text;
        [SerializeField] private float m_InputIntervalTime = 0.05f;
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
            int len = m_Words.Length;
            float totTime = intervalTime * len;
            float stTime = Time.time;
            do {
                int curPos;
                if (totTime == 0) curPos = len;
                else curPos = (int)((Time.time - stTime) / totTime * len);
                curPos = Mathf.Min(curPos, len);
                m_Text.text = m_Words.Substring(0, curPos);
                if (curPos >= len) {
                    Invoke("HideDialogue", 3);
                    m_Coroutine = null;
                    yield break;
                }
                yield return new WaitForSeconds(intervalTime);
            } while (true);
        }

        private IEnumerator FadeOut() {
            float alpha;
            do {
                alpha = m_Text.color.a - m_FadeOutSpeed * Time.deltaTime;
                SetAlpha(Mathf.Clamp01(alpha));
                yield return null;
            } while (alpha > 0);
            m_Coroutine = null;
            yield break;
        }

        public void ShowDialogue(string content, Color? color = null, float? intervalTime = null) {
            m_Text.color = color ?? Color.white;
            if (m_Coroutine != null) {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
            m_Words = content;
            m_Coroutine = StartCoroutine(FadeIn(intervalTime ?? m_InputIntervalTime));
        }
        private void HideDialogue() {
            m_Coroutine = StartCoroutine(FadeOut());
        }
    }
}
