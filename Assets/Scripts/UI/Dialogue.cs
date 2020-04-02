using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI {
    public class Dialogue : MonoBehaviour {
        private static Dialogue _mainInstance;
        public float showDuaration = 3f;            //对话显示时间
        public float inputIntervalTime = 0.05f;     //打字效果（文字输入）的间隔时间
        public float fadeOutDuration = 1f;          //对话淡出时间
        private string m_Content;
        private Coroutine m_Coroutine;

        public static Dialogue main {
            get {
                return _mainInstance ??
                    (_mainInstance = GameObject.FindGameObjectWithTag("MainDialogue").GetComponent<Dialogue>());
            }
        }

        public Text text { get; private set; }

        private void Awake() {
            text = GetComponent<Text>();
        }

        // Start is called before the first frame update
        void Start() {
            text.CrossFadeAlpha(0f, 0f, true);
        }

        private class RichTextChecker {
            private const string TAGS = "(a|b|i|size|color|material)";
            //private const string SINGLE_TAGS = "(quad)";

            private Regex tagPattern = new Regex(string.Format("</?{0}(=[^<>]+)?>", TAGS), RegexOptions.IgnoreCase);
            private Regex openTagPattern = new Regex(string.Format("<{0}(=[^<>]+)?>", TAGS), RegexOptions.IgnoreCase);
            private Regex closeTagPattern = new Regex(string.Format("</{0}(=[^<>]+)?>", TAGS), RegexOptions.IgnoreCase);
            //private Regex singleTagPattern = new Regex(string.Format("<{0}>", SINGLE_TAGS), RegexOptions.IgnoreCase);
            private Regex wordPattern = new Regex(TAGS);

            class Tag {
                public int index;
                public string tag;
                public string closeTag;
                public bool isOpen {
                    get {
                        if (closeTag != null) return true;
                        else return false;
                    }
                }
                public Tag(int idx, string tag) {
                    index = idx;
                    this.tag = tag;
                }
            }
            List<Tag> tagList = new List<Tag>();
            Stack<Tag> matchingTags = new Stack<Tag>();
            int curMatchedTagLenTot = 0;
            int curMatchIdx = 0;

            string content;
            StringBuilder pureText = new StringBuilder();
            bool isRich = false;

            public RichTextChecker(string content) {
                this.content = content;
                isRich = CheckRichText();
            }

            private bool CheckRichText() {
                var tagMatches = tagPattern.Matches(content);
                if (tagMatches.Count == 0 || (tagMatches.Count & 1) != 0) return false;
                int st = 0;
                Stack<string> tagStack = new Stack<string>();
                Stack<int> tagIdxStack = new Stack<int>();
                for (int i = 0; i < tagMatches.Count; i++) {
                    if (openTagPattern.IsMatch(tagMatches[i].Value)) {
                        tagStack.Push(wordPattern.Match(tagMatches[i].Value).Value);
                        tagIdxStack.Push(i);
                        int len = tagMatches[i].Index - st;
                        pureText.Append(content.Substring(st, len));
                        st += len + tagMatches[i].Length;
                        tagList.Add(new Tag(pureText.Length, tagMatches[i].Value));
                    }
                    else if (tagStack.Count > 0 && wordPattern.Match(tagMatches[i].Value).Value == tagStack.Peek()) {
                        tagStack.Pop();
                        tagList[tagIdxStack.Pop()].closeTag = tagMatches[i].Value;
                        int len = tagMatches[i].Index - st;
                        pureText.Append(content.Substring(st, len));
                        st += len + tagMatches[i].Length;
                        tagList.Add(new Tag(pureText.Length, tagMatches[i].Value));
                    }
                    else return false;
                }
                pureText.Append(content.Substring(st, content.Length - st));

                if (tagStack.Count > 0) return false;
                else return true;
            }

            public string PureText {
                get {
                    if (isRich) return pureText.ToString();
                    else return content;
                }
            }

            internal void ShowText(Text text, int curPos) {
                if (!isRich) {
                    text.text = content.Substring(0, curPos);
                    return;
                }

                while (curMatchIdx < tagList.Count && curPos >= tagList[curMatchIdx].index) {
                    if (tagList[curMatchIdx].isOpen) {
                        matchingTags.Push(tagList[curMatchIdx]);
                        curMatchedTagLenTot += tagList[curMatchIdx].tag.Length;
                    }
                    else {
                        matchingTags.Pop();
                        curMatchedTagLenTot += tagList[curMatchIdx].tag.Length;
                    }
                    curMatchIdx++;
                }

                StringBuilder sb = new StringBuilder(content.Substring(0, curPos + curMatchedTagLenTot));
                foreach(var tag in matchingTags) {
                    sb.Append(tag.closeTag);
                }
                text.text = sb.ToString();
            }
        }

        private IEnumerator FadeIn(float intervalTime) {
            text.CrossFadeAlpha(1f, 0f, true);

            RichTextChecker checker = new RichTextChecker(m_Content);

            int len = checker.PureText.Length;
            float totTime = intervalTime * (len - 1);
            float stTime = Time.time;

            do {
                int curPos;
                if (Mathf.Approximately(totTime, 0)) curPos = len;
                else curPos = Mathf.FloorToInt(Mathf.Lerp(0, len, (Time.time - stTime) / totTime)) + 1;
                curPos = Mathf.Min(curPos, len);

                checker.ShowText(text, curPos);

                if (curPos >= len) yield break;
                yield return null;
            } while (true);
        }

        public void ShowDialogue(string content, Color? color = null, float? intervalTime = null) {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(content = content.Trim())) return;
            text.color = color ?? Color.white;
            if (m_Coroutine != null) {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
            m_Content = content;
            m_Coroutine = StartCoroutine(Display(intervalTime ?? inputIntervalTime));
        }

        private IEnumerator Display(float intervalTime) {
            yield return StartCoroutine(FadeIn(intervalTime));
            yield return new WaitForSecondsRealtime(showDuaration);
            yield return HideDialogue();
            m_Coroutine = null;
        }

        public IEnumerator HideDialogue(float fadeDuration = -1) {
            fadeDuration = fadeDuration >= 0 ? fadeDuration : fadeOutDuration;
            text.CrossFadeAlpha(0f, fadeDuration, true);
            yield return new WaitForSecondsRealtime(fadeDuration);
        }
    }
}
