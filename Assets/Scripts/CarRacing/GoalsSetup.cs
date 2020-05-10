using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace MyGameApplication.CarRacing {
    public class GoalsSetup : MonoBehaviour {
        private WaypointCircuit m_Circuit;

        private void Awake() {
            m_Circuit = GetComponent<WaypointCircuit>();

            ReachGoal.rankCal = FindObjectOfType<RankCalculator>();
            ReachGoal.rankCal.destinations = new Transform[transform.childCount];
        }

        // Start is called before the first frame update
        void Start() {
            var size = new Vector3(50, 50, 0);
            var children = transform.GetChildren();

            for (int i = 0; i < children.Length - 1; i++) {
                var child = children[i].gameObject;

                if (!child.GetComponent<BoxCollider>()) {
                    var point = m_Circuit.GetRoutePoint(m_Circuit.Distances[i] - 0.05f);
                    children[i].rotation = Quaternion.LookRotation(point.direction);

                    var collider = child.AddComponent<BoxCollider>();
                    collider.isTrigger = true;
                    collider.size = size;
                }

                var goal = child.AddComponent<ReachGoal>();
                goal.identifier = i;

                ReachGoal.rankCal.destinations[i] = children[i + 1];
            }
        }
    }
}
