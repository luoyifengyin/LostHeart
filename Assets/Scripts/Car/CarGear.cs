using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Car {
    [RequireComponent(typeof(CarController))]

    public class CarGear : MonoBehaviour {
        private CarController m_Car;
        [SerializeField] private float m_SpeedOfMaxGear = 25f;
        [SerializeField] private float m_RevRangeBoundary = 1f;
        private static int NoOfGears = 5;
        private int m_GearNum;
        private float m_GearFactor;

        public float Revs { get; private set; }

        private void Awake() {
            m_Car = GetComponent<CarController>();
        }

        void FixedUpdate() {
            GearChanging();
            CalculateRevs();
        }

        private void GearChanging() {
            float f = Mathf.Abs(m_Car.CurrentSpeed / m_SpeedOfMaxGear);
            float upgearlimit = (1 / (float)NoOfGears) * (m_GearNum + 1);
            float downgearlimit = (1 / (float)NoOfGears) * m_GearNum;

            if (m_GearNum > 0 && f < downgearlimit) {
                m_GearNum--;
            }

            if (f > upgearlimit && (m_GearNum < (NoOfGears - 1))) {
                m_GearNum++;
            }
        }

        // simple function to add a curved bias towards 1 for a value in the 0-1 range
        private static float CurveFactor(float factor) {
            return 1 - (1 - factor) * (1 - factor);
        }

        // unclamped version of Lerp, to allow value to exceed the from-to range
        private static float ULerp(float from, float to, float value) {
            return (1.0f - value) * from + value * to;
        }

        private void CalculateGearFactor() {
            float f = (1 / (float)NoOfGears);
            // gear factor is a normalised representation of the current speed within the current gear's range of speeds.
            // We smooth towards the 'target' gear factor, so that revs don't instantly snap up or down when changing gear.
            var targetGearFactor = Mathf.InverseLerp(f * m_GearNum, f * (m_GearNum + 1), Mathf.Abs(m_Car.CurrentSpeed / m_SpeedOfMaxGear));
            m_GearFactor = Mathf.Lerp(m_GearFactor, targetGearFactor, Time.deltaTime * 5f);
        }

        private void CalculateRevs() {
            // calculate engine revs (for display / sound)
            // (this is done in retrospect - revs are not used in force/power calculations)
            CalculateGearFactor();
            var gearNumFactor = m_GearNum / (float)NoOfGears;
            var revsRangeMin = ULerp(0f, m_RevRangeBoundary, CurveFactor(gearNumFactor));
            var revsRangeMax = ULerp(m_RevRangeBoundary, 1f, gearNumFactor);
            Revs = ULerp(revsRangeMin, revsRangeMax, m_GearFactor);
        }
    }
}
