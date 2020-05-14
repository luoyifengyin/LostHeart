using MyGameApplication.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

namespace MyGameApplication.Car {
    public class CarInfoDisplayer : MonoBehaviour {
        private CarController m_CarController;
        private const string SPEED_MSG = "车速: {0}";
        private const string TORQUE_MSG = "车轮力矩: {0}";

        private WaypointProgressTracker m_Tracker;
        private const string DISTANCE_MSG = "路程：{0}";

        private void Awake() {
            m_CarController = GetComponentInParent<CarController>();
            m_Tracker = GetComponentInParent<WaypointProgressTracker>();
        }

        private void LateUpdate() {
            GameDataInfo.Display(string.Format(SPEED_MSG, m_CarController.CurrentSpeed));
            GameDataInfo.Display(string.Format(TORQUE_MSG, m_CarController.CurrentTorque));

            if (m_Tracker) GameDataInfo.Display(string.Format(DISTANCE_MSG, m_Tracker.ProgressDistance));
        }
    }
}
