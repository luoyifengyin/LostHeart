using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication {
    public class VisibleTool : MonoBehaviour {
        public bool Visible { get; private set; }
        public event Action BecameVisible;
        public event Action BecameInvisible;

        private void OnBecameVisible() {
            Visible = true;
            BecameVisible?.Invoke();
        }
        private void OnBecameInvisible() {
            Visible = false;
            BecameInvisible?.Invoke();
        }
    }
}
