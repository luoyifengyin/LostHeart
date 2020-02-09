using MyGameApplication.UI.ItemBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class UIController : MonoBehaviour {
        [SerializeField] private Knapsack m_Knapsack = null;

        private void Awake() {
            if (!m_Knapsack) m_Knapsack = FindObjectOfType<Knapsack>();
        }

        // Update is called once per frame
        void Update() {
            if (m_Knapsack && CrossPlatformInputManager.GetButtonDown("Cancel")) {
                m_Knapsack.gameObject.SetActive(!m_Knapsack.gameObject.activeSelf);
            }
        }
    }
}
