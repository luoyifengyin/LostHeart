using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace MyGameApplication.CarRacing {
    public class EnemyCar : MonoBehaviour {
        private WaypointProgressTracker m_Tracker;

        public int StartOrder { get; private set; }

        private void Awake() {
            m_Tracker = GetComponent<WaypointProgressTracker>();
        }

        private void Start() {
            StartOrder = Mathf.Max(m_Tracker.ProgressNum, 0);
            gameObject.SetActive(false);
        }

        private void Update() {
            
        }
    }
}
