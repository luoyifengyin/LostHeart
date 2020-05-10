using MyGameApplication.Car;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace MyGameApplication.CarRacing {
    public class ReachGoal : MonoBehaviour {
        public static RankCalculator rankCal;

        public int identifier;

        private void OnTriggerStay(Collider other) {
            if (other.CompareTag("Car")) {
                Vector3 pos = transform.InverseTransformPoint(other.transform.position);
                if (pos.z > 0) {
                    rankCal.racers[other.attachedRigidbody.gameObject].segmentation = identifier;
                }
                else {
                    if (identifier > 0)
                        rankCal.racers[other.attachedRigidbody.gameObject].segmentation = identifier - 1;
                }
            }
        }
    }
}
