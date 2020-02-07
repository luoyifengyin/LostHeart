using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI {
    public class Dialogue : MonoBehaviour {
        private Text m_Text;
        [SerializeField] private float m_InputIntervalTime = 0.05f;
        [SerializeField] private float m_FadeOutDuration = 1f;
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
        }

        private IEnumerator FadeIn(float intervalTime) {
            SetAlpha(1f);
            int len = m_Words.Length;
            float totTime = intervalTime * (len - 1);
            float stTime = Time.time;
            WaitForSeconds wait = new WaitForSeconds(intervalTime);
            do {
                int curPos;
                if (Mathf.Approximately(m_InputIntervalTime, 0)) curPos = len;
                else curPos = Mathf.CeilToInt(Mathf.Lerp(0, len, (Time.time - stTime) / totTime));
                curPos = Mathf.Min(curPos, len);
                m_Text.text = m_Words.Substring(0, curPos);
                if (curPos >= len) {
                    Invoke("HideDialogue", 3);
                    m_Coroutine = null;
                    yield break;
                }
                yield return wait;
            } while (true);
        }

        private IEnumerator FadeOut() {
            if (Mathf.Approximately(m_FadeOutDuration, 0)) {
                SetAlpha(0);
                yield break;
            }
            float alpha = m_Text.color.a;
            float speed = alpha / m_FadeOutDuration;
            do {
                alpha = Mathf.MoveTowards(alpha, 0, speed * Time.deltaTime);
                SetAlpha(Mathf.Clamp01(alpha));
                yield return null;
            } while (alpha > 0);
            m_Coroutine = null;
            yield break;
        }

        public void ShowDialogue(string content, Color? color = null, float? intervalTime = null) {
            if (string.IsNullOrEmpty(content)) return;
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
