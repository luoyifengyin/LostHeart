using MyGameApplication.UI.ItemBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class KnapsackControl : MonoBehaviour {
        private Animator m_Animator;
        private readonly int OPEN_HASH = Animator.StringToHash("Open");

        private void Awake() {
            m_Animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update() {
            if (CrossPlatformInputManager.GetButtonDown("Cancel")) {
                m_Animator.SetBool(OPEN_HASH, !m_Animator.GetBool(OPEN_HASH));
            }
        }
    }
}
