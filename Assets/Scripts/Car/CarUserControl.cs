using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Car {
    [RequireComponent(typeof(CarController))]

    public class CarUserControl : MonoBehaviour {
        private CarController m_Car;

        private void Awake() {
            m_Car = GetComponent<CarController>();
        }

        private void FixedUpdate() {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            float brake = CrossPlatformInputManager.GetAxis("Fire3");
            m_Car.Move(h, v);
            if (brake > 0.5) m_Car.Brake();
        }
    }
}
