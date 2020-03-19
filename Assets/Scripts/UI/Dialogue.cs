using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI {
    public class Dialogue : MonoBehaviour {
        private static Dialogue _mainInstance;
        public float showDuaration = 3f;            //对话显示时间
        public float inputIntervalTime = 0.05f;     //打字效果（文字输入）的间隔时间
        public float fadeOutDuration = 1f;          //对话淡出时间
        private string m_Words;
        private Coroutine m_Coroutine;

        public static Dialogue main {
            get {
                return _mainInstance ??
                    (_mainInstance = GameObject.FindGameObjectWithTag("MainDialogue").GetComponent<Dialogue>());
            }
        }

        public Text text { get; private set; }

        //private void SetAlpha(float alpha) {
        //    Color color = text.color;
        //    text.color = new Color(color.r, color.b, color.g, alpha);
        //}

        private void Awake() {
            text = GetComponent<Text>();
        }

        // Start is called before the first frame update
        void Start() {
            //SetAlpha(0f);
            text.CrossFadeAlpha(0f, 0f, true);
        }

        private IEnumerator FadeIn(float intervalTime) {
            //SetAlpha(1f);
            text.CrossFadeAlpha(1f, 0f, true);
            int len = m_Words.Length;
            float totTime = intervalTime * (len - 1);
            float stTime = Time.time;
            WaitForSeconds wait = new WaitForSeconds(intervalTime);
            do {
                int curPos;
                if (Mathf.Approximately(totTime, 0)) curPos = len;
                else curPos = Mathf.CeilToInt(Mathf.Lerp(0, len, (Time.time - stTime) / totTime));
                curPos = Mathf.Min(curPos, len);
                text.text = m_Words.Substring(0, curPos);
                if (curPos >= len) yield break;
                yield return wait;
            } while (true);
        }

        //private IEnumerator FadeOut(float duration) {
        //    if (Mathf.Approximately(duration, 0)) {
        //        SetAlpha(0);
        //        yield break;
        //    }
        //    float alpha = text.color.a;
        //    float speed = alpha / duration;
        //    do {
        //        alpha = Mathf.MoveTowards(alpha, 0, speed * Time.deltaTime);
        //        SetAlpha(Mathf.Clamp01(alpha));
        //        yield return null;
        //    } while (alpha > 0);
        //    m_Coroutine = null;
        //    yield break;
        //}

        public void ShowDialogue(string content, Color? color = null, float? intervalTime = null) {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(content = content.Trim())) return;
            text.color = color ?? Color.white;
            if (m_Coroutine != null) {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
            m_Words = content;
            m_Coroutine = StartCoroutine(Display(intervalTime ?? inputIntervalTime));
        }
        private IEnumerator Display(float intervalTime) {
            yield return StartCoroutine(FadeIn(intervalTime));
            yield return new WaitForSecondsRealtime(showDuaration);
            HideDialogue();
            m_Coroutine = null;
        }
        public void HideDialogue(float fadeDuration = -1) {
            fadeDuration = fadeDuration >= 0 ? fadeDuration : fadeOutDuration;
            //m_Coroutine = StartCoroutine(FadeOut(fadeDuration));
            text.CrossFadeAlpha(0f, fadeDuration, true);
        }
    }
}
