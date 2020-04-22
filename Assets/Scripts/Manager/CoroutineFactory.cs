using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

namespace MyGameApplication.Manager {
    public class CoroutineFactory : MonoBehaviour {
        public static CoroutineFactory Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        public static Coroutine Start(IEnumerator routine) {
            return Instance.StartCoroutine(routine);
        }

        public static void Stop(Coroutine routine) {
            Instance.StopCoroutine(routine);
        }
        public static void Stop(IEnumerator routine) {
            Instance.StopCoroutine(routine);
        }
    }
}
