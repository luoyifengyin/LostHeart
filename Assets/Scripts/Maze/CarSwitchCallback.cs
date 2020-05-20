using MyGameApplication.Car;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Maze {
    public class CarSwitchCallback : MonoBehaviour {
        private CarUserControl m_CarControl;
        private CarAudio m_CarAudio;
        private Skid m_Skid;
        private HeadLight m_HeadLight;
        private BrakeLight m_BrakeLight;
        private Rigidbody m_Rigidbody;

        private void Awake() {
            m_CarControl = GetComponent<CarUserControl>();
            m_CarAudio = GetComponent<CarAudio>();
            m_Skid = GetComponent<Skid>();
            m_HeadLight = GetComponentInChildren<HeadLight>();
            m_BrakeLight = GetComponentInChildren<BrakeLight>();
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void OnControlEnable() {
            m_CarControl.enabled = true;
            m_CarAudio.enabled = true;
            m_CarAudio.StartSound();
            m_Skid.enabled = true;
            m_HeadLight.enabled = true;
            m_BrakeLight.enabled = true;
            var carUserGetOut = GetComponent<CarUserGetOut>();
            if (carUserGetOut) carUserGetOut.enabled = true;
        }

        private void OnControlDisable() {
            m_CarControl.enabled = false;
            m_CarAudio.enabled = false;
            m_CarAudio.StopSound();
            m_Skid.enabled = false;
            m_HeadLight.enabled = false;
            m_BrakeLight.enabled = false;
            var carUserGetOut = GetComponent<CarUserGetOut>();
            if (carUserGetOut) carUserGetOut.enabled = false;

            m_Rigidbody.velocity = new Vector3(0, 0, 0);
        }

        private void OnEnable() {
            PlayerControlManager.Instance.AddSwitchCallback(gameObject, OnControlEnable, OnControlDisable);
        }

        private void OnDisable() {
            PlayerControlManager.Instance.RemoveSwitchCallback(gameObject, OnControlEnable, OnControlDisable);
        }
    }
}
