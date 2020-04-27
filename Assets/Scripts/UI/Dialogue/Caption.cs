using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.UI {
    public class Caption : Dialogue {
        public float showDuaration = 3f;            //对话显示时间
        public float fadeOutDuration = 1f;          //对话淡出时间

        private Typewriter m_Typewriter;

        void Start() {
            m_Typewriter = new Typewriter(text);
            text.CrossFadeAlpha(0f, 0f, true);
        }

        protected override IEnumerator OnShow() {
            text.text = "";
            text.CrossFadeAlpha(1f, 0f, true);
            yield return m_Typewriter.Typewrite(content);
        }

        protected override IEnumerator OnWait() {
            yield return new WaitForSecondsRealtime(showDuaration);
        }
        protected override IEnumerator OnHide() {
            text.CrossFadeAlpha(0f, fadeOutDuration, true);
            yield return new WaitForSecondsRealtime(fadeOutDuration);
        }
    }
}
