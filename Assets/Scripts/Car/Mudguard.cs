using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Car {
    public class Mudguard : MonoBehaviour {
        [SerializeField] private GameObject m_Wheel = null;
        private CarController m_Car;

        private Vector3 m_TargetOriginalPosition;
        private Vector3 m_Origin;
        private Quaternion m_OriginalRotation;

        // Start is called before the first frame update
        void Start() {
            m_TargetOriginalPosition = m_Wheel.transform.localPosition;
            m_Origin = transform.localPosition;
            m_Car = GetComponentInParent<CarController>();
            m_OriginalRotation = transform.localRotation;
        }

        private void LateUpdate() {
            transform.localPosition = m_Origin + (m_Wheel.transform.localPosition - m_TargetOriginalPosition);
            transform.localRotation = m_OriginalRotation * Quaternion.Euler(0, m_Car.SteerAngle, 0);
        }
    }
}
