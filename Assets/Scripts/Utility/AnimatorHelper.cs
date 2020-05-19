using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Utility {
    public static class AnimatorHash {
        public static readonly int OPEN = Animator.StringToHash("Open");
        public static readonly int CLOSED = Animator.StringToHash("Closed");

        public static readonly int START = Animator.StringToHash("Start");
        public static readonly int END = Animator.StringToHash("End");

        public static readonly int FADE_IN = Animator.StringToHash("FadeIn");
        public static readonly int FADE_OUT = Animator.StringToHash("FadeOut");

    }

    public static class AnimatorHelper {
        public static CustomYieldInstruction CreateWaitTransition(this Animator animator, int layer = 0) {
            return new WaitWhile(() => animator.IsInTransition(layer));
        }

        public static CustomYieldInstruction CreateWaitFinish(this Animator animator, int hash, int layer = 0) {
            return new WaitUntil(() => {
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);
                return info.shortNameHash == hash && info.normalizedTime > 1.0f;
            });
        }
    }
}
