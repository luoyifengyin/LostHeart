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

        private AntiRollBar[] m_AntiRollBars;
        private const string TRAVEL_MSG = "车轮悬挂系数: {0}  {1}";

        private WaypointProgressTracker m_Tracker;
        private const string DISTANCE_MSG = "路程：{0}";

        private void Awake() {
            m_CarController = GetComponentInParent<CarController>();
            m_Tracker = GetComponentInParent<WaypointProgressTracker>();
        }

        private void Start() {
            m_AntiRollBars = GetComponentsInParent<AntiRollBar>();
        }

        private void LateUpdate() {
            GameDataInfo.Display(string.Format(SPEED_MSG, m_CarController.CurrentSpeed));
            GameDataInfo.Display(string.Format(TORQUE_MSG, m_CarController.CurrentTorque));

            if (m_AntiRollBars[0].enabled)
                GameDataInfo.Display(string.Format("前" + TRAVEL_MSG, m_AntiRollBars[0].travelL, m_AntiRollBars[0].travelR));
            if (m_AntiRollBars[1].enabled)
                GameDataInfo.Display(string.Format("后"+TRAVEL_MSG, m_AntiRollBars[1].travelL, m_AntiRollBars[1].travelR));

            if (m_Tracker) GameDataInfo.Display(string.Format(DISTANCE_MSG, m_Tracker.ProgressDistance));
        }
    }
}
