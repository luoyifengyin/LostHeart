using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.UI {
    public class Fader : MonoBehaviour {
        public static Fader Instance { get; private set; }

        private CanvasGroup faderCanvasGroup;
        private Coroutine coroutine;

        public float Alpha { get { return faderCanvasGroup.alpha; } set { faderCanvasGroup.alpha = value; } }
        public bool IsFading { get; private set; }

        private void Awake() {
            Instance = this;
            faderCanvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        public Coroutine Fade(float finalAlpha, float duration) {
            if (IsFading) StopCoroutine(coroutine);
            float speed = Mathf.Abs(Alpha - finalAlpha) / duration;
            return coroutine = StartCoroutine(Fading(finalAlpha, speed));
        }

        private IEnumerator Fading(float finalAlpha, float speed) {
            IsFading = true;
            while (!Mathf.Approximately(Alpha, finalAlpha)) {
                Alpha = Mathf.MoveTowards(Alpha, finalAlpha, speed * Time.deltaTime);
                yield return null;
            }
            IsFading = false;
        }
    }
}
