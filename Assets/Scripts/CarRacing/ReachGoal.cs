using MyGameApplication.Car;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.CarRacing {
    public class ReachGoal : MonoBehaviour {
        private static RankCalculator m_RankCal;

        private void Awake() {
            if (!m_RankCal) m_RankCal = FindObjectOfType<RankCalculator>();
        }

        private void OnTriggerStay(Collider other) {
            if (other.CompareTag("Car")) {
                Vector3 pos = transform.InverseTransformPoint(other.transform.position);
                if (pos.z > 0) {
                    m_RankCal.racers[other.gameObject].segmentation++;
                }
                else {
                    m_RankCal.racers[other.gameObject].segmentation--;
                }
            }
        }
    }
}
