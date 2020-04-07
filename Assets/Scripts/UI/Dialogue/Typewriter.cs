using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI {
    public abstract class Typewriter : MonoBehaviour, ITalk {
        public Text text;
        public float typeIntervalTime = 0.05f;      //打字效果（文字输入）的间隔时间
        private string m_Content;
        private Coroutine m_Coroutine;

        private void Awake() {
            if (!text) text = GetComponentInChildren<Text>();
        }

        private class RichTextChecker {
            private const string TAGS = "(b|i|size|color|material)";
            //private const string SINGLE_TAGS = "(quad)";

            private readonly Regex tagPattern = new Regex(string.Format("</?{0}(=[^<>]+)?>", TAGS), RegexOptions.IgnoreCase);
            //private readonly Regex openTagPattern = new Regex(string.Format("<{0}(=[^<>]+)?>", TAGS), RegexOptions.IgnoreCase);
            //private readonly Regex closeTagPattern = new Regex(string.Format("</{0}(=[^<>]+)?>", TAGS), RegexOptions.IgnoreCase);
            //private readonly Regex singleTagPattern = new Regex(string.Format("<{0}>", SINGLE_TAGS), RegexOptions.IgnoreCase);
            private readonly Regex wordPattern = new Regex(string.Format("(?<=^</?){0}", TAGS), RegexOptions.IgnoreCase);

            private class Tag {
                internal int index;         //该标签处于纯文本中的位置
                internal string tag;        //标签本体
                internal string closeTag;   //开始标签对应的结束标签
                public bool isOpen {        //是否为开始标签
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
            private List<Tag> tagList = new List<Tag>();            //按出现顺序存储的标签列表
            private Stack<Tag> matchingTagStack = new Stack<Tag>(); //富文本标签栈
            private int curMatchIdx = 0;                            //当前遍历到的tagList的下标
            private int curMatchedTagLenTot = 0;                    //当前累计遍历的标签长度总和

            private string content;                                 //对话内容（原文本）
            private StringBuilder pureText = new StringBuilder();   //纯文本
            private bool isRich = false;                            //是否为富文本

            private void Init() {
                tagList.Clear();
                matchingTagStack.Clear();
                curMatchIdx = 0;
                curMatchedTagLenTot = 0;
                content = null;
                pureText.Clear();
                isRich = false;
                prePos = 0;
                preText.Clear();
            }

            //检查是否为富文本，标签是否匹配
            public bool CheckRichText(string content) {
                Init();
                this.content = content;
                var tagMatches = tagPattern.Matches(content);   //首先筛选出所有的标签
                if (tagMatches.Count == 0 || (tagMatches.Count & 1) != 0) return false;
                int st = 0;
                Stack<string> tagStack = new Stack<string>();
                Stack<int> tagIdxStack = new Stack<int>();
                for (int i = 0; i < tagMatches.Count; i++) {
                    if (tagMatches[i].Value[1] != '/') {
                        //如果是开始标签，则压入栈中
                        tagStack.Push(wordPattern.Match(tagMatches[i].Value).Value);
                        tagIdxStack.Push(i);
                        int len = tagMatches[i].Index - st;
                        pureText.Append(content.Substring(st, len));
                        st += len + tagMatches[i].Length;
                        tagList.Add(new Tag(pureText.Length, tagMatches[i].Value));
                    }
                    else if (tagStack.Count > 0 && wordPattern.Match(tagMatches[i].Value).Value == tagStack.Peek()) {
                        //如果是结束标签且与栈顶的开始标签匹配，则从栈中弹出标签
                        tagStack.Pop();
                        tagList[tagIdxStack.Pop()].closeTag = tagMatches[i].Value;  //记下开始标签所对应的结束标签
                        int len = tagMatches[i].Index - st;
                        pureText.Append(content.Substring(st, len));
                        st += len + tagMatches[i].Length;
                        tagList.Add(new Tag(pureText.Length, tagMatches[i].Value));
                    }
                    else return false;
                }
                pureText.Append(content.Substring(st, content.Length - st));

                if (tagStack.Count > 0) return false;
                else return isRich = true;
            }

            public string PureText {
                get {
                    if (isRich) return pureText.ToString();
                    else return content;
                }
            }

            private int prePos = 0;
            private StringBuilder preText = new StringBuilder();
            public void ShowText(Text text, int curPos) {
                if (!isRich) {
                    text.text = content.Substring(0, curPos);
                    return;
                }

                while (curMatchIdx < tagList.Count && curPos >= tagList[curMatchIdx].index) {
                    if (tagList[curMatchIdx].isOpen) {
                        //如果遇到开始标签，则压入富文本标签栈中
                        matchingTagStack.Push(tagList[curMatchIdx]);
                        curMatchedTagLenTot += tagList[curMatchIdx].tag.Length;
                    }
                    else {
                        //否则，弹出标签
                        matchingTagStack.Pop();
                        curMatchedTagLenTot += tagList[curMatchIdx].tag.Length;
                    }
                    curMatchIdx++;
                }

                preText.Append(content.Substring(prePos, curPos + curMatchedTagLenTot - prePos));
                prePos = curPos + curMatchedTagLenTot;
                StringBuilder sb = new StringBuilder(preText.ToString());
                foreach (var tag in matchingTagStack) {
                    sb.Append(tag.closeTag);
                }
                text.text = sb.ToString();
            }
        }

        private RichTextChecker m_RichTextChecker = new RichTextChecker();
        private bool m_DisplayImmediately = false;

        private IEnumerator Typewrite() {
            m_DisplayImmediately = false;
            m_RichTextChecker.CheckRichText(m_Content);

            int len = m_RichTextChecker.PureText.Length;
            float stTime = Time.time;
            do {
                int curPos;
                if (Mathf.Approximately(typeIntervalTime, 0)) curPos = len;
                else curPos = Mathf.FloorToInt((Time.time - stTime) / typeIntervalTime) + 1;
                curPos = Mathf.Min(curPos, len);

                if (m_DisplayImmediately) {
                    m_DisplayImmediately = false;
                    curPos = len;
                }

                m_RichTextChecker.ShowText(text, curPos);

                if (curPos >= len) yield break;
                yield return null;
            } while (true);
        }

        public Coroutine ShowDialogue(string content, Color? color = null) {
            if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(content = content.Trim())) return null;
            text.color = color ?? Color.white;
            if (m_Coroutine != null) {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
            m_Content = content;
            return m_Coroutine = StartCoroutine(Display());
        }

        public virtual Coroutine ShowDialogue(string content) {
            return ShowDialogue(content, Color.white);
        }

        private IEnumerator Display() {
            yield return OnShow();
            yield return StartCoroutine(Typewrite());
            yield return OnWait();
            yield return OnHide();
            m_Coroutine = null;
        }

        public virtual Coroutine HideDialogue() {
            if (m_Coroutine != null) StopCoroutine(m_Coroutine);
            return m_Coroutine = StartCoroutine(OnHide());
        }

        protected abstract IEnumerator OnShow();

        protected abstract IEnumerator OnWait();

        protected abstract IEnumerator OnHide();

        public void DisplayImmediately() {
            m_DisplayImmediately = true;
        }
    }
}
