using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameApplication.Car;

namespace MyGameApplication.Item {
    public class Accelerator : Prop {
        private CarController m_Car;

        public override bool isCarItem() {
            return true;
        }

        public override bool UseCondition() {
            if (!m_Car) {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player) m_Car = player.GetComponent<CarController>();
            }
            if (m_Car) return true;
            return false;
        }

        public override void PayLoad() {
            base.PayLoad();
            if (m_Car.AccelInput > 0) {
                m_Car.Accelerate();
            }
            expire();
        }
    }
}
