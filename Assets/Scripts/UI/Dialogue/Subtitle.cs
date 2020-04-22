using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.UI {
    public class Subtitle : Typewriter {
        public float showDuaration = 3f;            //对话显示时间
        public float fadeOutDuration = 1f;          //对话淡出时间

        void Start() {
            text.CrossFadeAlpha(0f, 0f, true);
        }

        protected override IEnumerator OnShow() {
            text.text = "";
            text.CrossFadeAlpha(1f, 0f, true);
            yield break;
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
