using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Maze {
    public class DayAndNight : MonoBehaviour {
        public static DayAndNight Instance { get; private set; }

        public float speed = 0.02f;

        [SerializeField] [Range(-90, 90)] private float m_NightToDayTerminator = 0;
        [SerializeField] [Range(90, 270)] private float m_DayToNightTerminator = 180;

        public bool IsDaytime { get; private set; }

        public event Action OnDaySwitch;
        public event Action OnNightSwitch;

        private void Awake() {
            Instance = this;

            float x = transform.rotation.eulerAngles.x;
            if (m_NightToDayTerminator <= x && x < m_DayToNightTerminator) {
                IsDaytime = true;
            }
        }

        void Update() {
            transform.Rotate(Vector3.right * speed * Time.deltaTime);
            float x = transform.eulerAngles.x;

            if (m_NightToDayTerminator <= x && x < m_DayToNightTerminator) {
                if (!IsDaytime) {
                    IsDaytime = true;
                    OnDaySwitch?.Invoke();
                }
            }
            else if (IsDaytime) {
                IsDaytime = false;
                OnNightSwitch?.Invoke();
            }
        }
    }
}
