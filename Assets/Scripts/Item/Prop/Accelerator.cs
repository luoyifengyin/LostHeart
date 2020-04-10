using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameApplication.Car;

namespace MyGameApplication.Item {
    public class Accelerator : Prop {
        [SerializeField] private float m_AccelForce = 0;
        [SerializeField] private float m_AccelPitchMultiplier = 1.25f;
        [SerializeField] private float m_AccelDuration = 5;
        private CarController m_Car;
        private Rigidbody m_CarRb;
        private CarAudio m_CarAudio;
        private float m_OriginalDrag;
        private float m_OriginalForwardTorque;
        private float m_OriginalHighPitchMultiplier;
        private float m_AcceleratingTime = 0;
        private Coroutine m_AccelCoroutine;
        private Coroutine m_DecelCoroutine;
        public static Accelerator progressing;

        public override bool isCarItem() {
            return true;
        }

        public bool IsAccelerating { get { return m_AccelCoroutine != null; } }
        public bool IsDecelerating { get { return m_DecelCoroutine != null; } }

        public override bool Condition() {
            if (!m_Car) {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player) m_Car = player.GetComponent<CarController>();
            }
            if (m_Car) return true;
            return false;
        }

        protected override void Operation() {
            if (m_Car.AccelInput > 0) {
                if (progressing) progressing.Stop();
                m_CarRb = m_Car.GetComponent<Rigidbody>();
                m_CarAudio = m_Car.GetComponent<CarAudio>();
                m_OriginalDrag = m_CarRb.drag;
                m_OriginalForwardTorque = m_Car.ForwardTorque;
                m_OriginalHighPitchMultiplier = m_CarAudio.highPitchMultiplier;
                Accelerate();
                progressing = this;
            }
            else Release();
        }

        private IEnumerator Accelerating() {
            float countDown = m_AccelDuration;
            float audioDuration = m_AccelDuration / 5;
            float audioTime = 0;
            while (countDown > 0 && m_Car.AccelInput > 0) {
                m_AcceleratingTime += Time.deltaTime;
                countDown -= Time.deltaTime;
                m_CarRb.AddForce(new Vector3(0, 0, m_AccelForce * (countDown / m_AccelDuration) * m_Car.AccelInput));

                if (m_CarAudio) {
                    audioTime += Time.deltaTime;
                    m_CarAudio.highPitchMultiplier = m_OriginalHighPitchMultiplier *
                        Mathf.Lerp(1, m_AccelPitchMultiplier, audioTime / audioDuration);
                }
                yield return null;
            }
            Expire();
        }

        public void Accelerate() {
            //print("accel");
            m_CarRb.drag /= 2f;
            m_Car.ForwardTorque *= 2f;
            m_AcceleratingTime = 0;
            m_AccelCoroutine = StartCoroutine(Accelerating());
        }

        public override void Expire() {
            if (m_AccelCoroutine != null) {
                StopCoroutine(m_AccelCoroutine);
                m_AccelCoroutine = null;
                Decelerate();
            }
        }

        private IEnumerator Decelerating() {
            float audioDuration = Mathf.Min(m_AccelDuration / 5, m_AcceleratingTime);
            float audioTime = audioDuration;
            while (audioTime > 0) {
                yield return null;
                audioTime -= Time.deltaTime;
                m_CarAudio.highPitchMultiplier = m_OriginalHighPitchMultiplier *
                    Mathf.Lerp(1, m_AccelPitchMultiplier, audioTime / audioDuration);
            }
            m_CarAudio.highPitchMultiplier = m_OriginalHighPitchMultiplier;
            m_DecelCoroutine = null;
            progressing = null;
            Release();
        }

        private void Decelerate() {
            //print("decel");
            m_CarRb.drag = m_OriginalDrag;
            m_Car.ForwardTorque = m_OriginalForwardTorque;
            if (m_CarAudio) m_DecelCoroutine = StartCoroutine(Decelerating());
        }

        public override void Stop() {
            base.Stop();
            if (m_DecelCoroutine != null) {
                StopCoroutine(m_DecelCoroutine);
                m_DecelCoroutine = null;
                m_CarAudio.highPitchMultiplier = m_OriginalHighPitchMultiplier;
                progressing = null;
                Release();
            }
        }
    }
}
