using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car {
    public class BrakeLight : MonoBehaviour {
        private CarController m_Car;
        private Renderer m_Renderer;

        private void Awake() {
            m_Car = GetComponentInParent<CarController>();
            m_Renderer = GetComponent<Renderer>();
        }

        void Update() {
            m_Renderer.enabled = m_Car.AccelInput < 0f || m_Car.Braking;
        }
    }
}
