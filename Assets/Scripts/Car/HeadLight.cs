using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Car {
    public class HeadLight : MonoBehaviour {
        [SerializeField] private GameObject m_Light;
        private Renderer m_Renderer;
        private GameObject[] m_HeadLights;
        private int m_Cnt;

        private void Awake() {
            m_Renderer = GetComponent<Renderer>();
            m_Cnt = m_Light.transform.childCount;
            m_HeadLights = new GameObject[m_Cnt];
            for (int i = 0; i < m_Cnt; i++) {
                m_HeadLights[i] = m_Light.transform.GetChild(i).gameObject;
            }
        }

        private void TurnLight(bool on) {
            m_Renderer.enabled = on;
            for (int i = 0; i < m_Cnt; i++) {
                m_HeadLights[i].SetActive(on);
            }
        }

        // Update is called once per frame
        void Update() {
            float open = CrossPlatformInputManager.GetAxis("Jump");
            if (open > 0.5) TurnLight(true);
            else TurnLight(false);
        }
    }
}
