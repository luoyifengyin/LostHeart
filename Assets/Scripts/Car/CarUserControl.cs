using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Car {
    [RequireComponent(typeof(CarController))]

    public class CarUserControl : MonoBehaviour {
        private CarController m_Car;
        private CarAudio m_CarAudio;
        private Rigidbody m_Rigidbody;

        private PlayerControlManager m_PlayerControlManager;

        private void Awake() {
            m_Car = GetComponent<CarController>();
            m_CarAudio = GetComponent<CarAudio>();
            m_Rigidbody = GetComponent<Rigidbody>();

            m_PlayerControlManager = PlayerControlManager.Instance;
            if (m_PlayerControlManager)
                m_PlayerControlManager.AddSwitchCallback(gameObject, OnControlEnable, OnControlDisable);
        }

        private void FixedUpdate() {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            float brake = CrossPlatformInputManager.GetAxis("Fire3");
            m_Car.Move(h, v);
            if (brake > 0) m_Car.Brake(brake);
        }

        [SerializeField] private HeadLight m_HeadLight;
        [SerializeField] private BrakeLight m_BrakeLight;
        private void OnControlEnable() {
            m_CarAudio.enabled = true;
            m_CarAudio.StartSound();
            enabled = true;
            m_HeadLight.enabled = true;
            m_BrakeLight.enabled = true;
        }
        private void OnControlDisable() {
            m_CarAudio.enabled = false;
            m_CarAudio.StopSound();
            enabled = false;
            m_HeadLight.enabled = false;
            m_BrakeLight.enabled = false;
            //m_Rigidbody.velocity = new Vector3(0, 0, 0);
        }

        private void OnDestroy() {
            if (m_PlayerControlManager)
                m_PlayerControlManager.RemoveSwitchCallback(gameObject, OnControlEnable, OnControlDisable);
        }
    }
}
