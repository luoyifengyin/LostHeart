using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

namespace Car {
    [RequireComponent(typeof(CarController))]

    public class Skid : MonoBehaviour {
        [SerializeField] private float m_SlipLimit = 0.3f;
        private WheelCollider[] m_WheelColliders;
        private WheelEffects[] m_WheelEffects;

        private void Awake() {
            m_WheelColliders = GetComponent<CarController>().WheelColliders;
            int len = m_WheelColliders.Length;
            m_WheelEffects = new WheelEffects[len];
            for (int i = 0; i < len; i++) {
                m_WheelEffects[i] = m_WheelColliders[i].GetComponent<WheelEffects>();
            }
        }

        // Update is called once per frame
        void Update() {
            CheckForWheelSpin();
        }

        // checks if the wheels are spinning and is so does three things
        // 1) emits particles
        // 2) plays tiure skidding sounds
        // 3) leaves skidmarks on the ground
        // these effects are controlled through the WheelEffects class
        private void CheckForWheelSpin() {
            // loop through all wheels
            for (int i = 0; i < 4; i++) {
                WheelHit wheelHit;
                m_WheelColliders[i].GetGroundHit(out wheelHit);

                // is the tire slipping above the given threshhold
                if (Mathf.Abs(wheelHit.forwardSlip) >= m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= m_SlipLimit) {
                    m_WheelEffects[i].EmitTyreSmoke();

                    // avoiding all four tires screeching at the same time
                    // if they do it can lead to some strange audio artefacts
                    if (!AnySkidSoundPlaying()) {
                        m_WheelEffects[i].PlayAudio();
                    }
                    continue;
                }

                // if it wasnt slipping stop all the audio
                if (m_WheelEffects[i].PlayingAudio) {
                    m_WheelEffects[i].StopAudio();
                }
                // end the trail generation
                m_WheelEffects[i].EndSkidTrail();
            }
        }

        private bool AnySkidSoundPlaying() {
            for (int i = 0; i < 4; i++) {
                if (m_WheelEffects[i].PlayingAudio) {
                    return true;
                }
            }
            return false;
        }
    }
}
