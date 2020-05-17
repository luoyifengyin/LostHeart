using System;
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

            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            void visFunc() { visCnt++; }
            void invisFunc() { visCnt--; }
            foreach (var renderer in renderers) {
                var vis = renderer.gameObject.AddComponent<VisibleTool>();
                vis.BecameVisible += visFunc;
                vis.BecameInvisible += invisFunc;
            }
        }

        private int visCnt = 0;
        public bool Visible {
            get => visCnt > 0;
        }
    }
}
