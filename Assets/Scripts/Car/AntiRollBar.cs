using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Car {
    public class AntiRollBar : MonoBehaviour {
        [SerializeField] private WheelCollider m_WheelL;
        [SerializeField] private WheelCollider m_WheelR;
        [SerializeField] private float m_AntiRoll = 5000;

        public AntiRollBar setWheels(WheelCollider wheelL, WheelCollider wheelR) {
            m_WheelL = wheelL;
            m_WheelR = wheelR;
            return this;
        }
        public AntiRollBar setAntiRoll(float val) {
            m_AntiRoll = val;
            return this;
        }

        private void FixedUpdate() {
            WheelHit hit;
            float travelL = 1.0f;
            float travelR = 1.0f;

            bool groundedL = m_WheelL.GetGroundHit(out hit);
            if (groundedL)
                travelL = (-m_WheelL.transform.InverseTransformPoint(hit.point).y - m_WheelL.radius) / m_WheelL.suspensionDistance;
            bool groundedR = m_WheelR.GetGroundHit(out hit);
            if (groundedR)
                travelR = (-m_WheelR.transform.InverseTransformPoint(hit.point).y - m_WheelR.radius) / m_WheelR.suspensionDistance;
            //print("travel: " + travelL + " " + travelR);

            float antiRollForce = (travelL - travelR) * m_AntiRoll;

            if (groundedL)
                m_WheelL.attachedRigidbody.AddForceAtPosition(m_WheelL.transform.up * -antiRollForce, m_WheelL.transform.position);
            if (groundedR)
                m_WheelR.attachedRigidbody.AddForceAtPosition(m_WheelR.transform.up * antiRollForce, m_WheelR.transform.position);
        }
    }
}
