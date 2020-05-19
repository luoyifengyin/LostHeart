using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Maze.NPC {
    public enum AppearTime {
        None = 0,
        Daytime = 1,
        Night = 2,
        AllDay = 3,
    }

    public class AppearCondition : MonoBehaviour {
        [SerializeField] private AppearTime appearTime = AppearTime.None;

        private void Start() {
            switch (appearTime) {
                case AppearTime.None:
                    gameObject.SetActive(false);
                    break;
                case AppearTime.Daytime:
                    DayAndNight.Instance.OnDaySwitch += Appear;
                    DayAndNight.Instance.OnNightSwitch += Disappear;
                    if (!DayAndNight.Instance.IsDaytime) Disappear();
                    break;
                case AppearTime.Night:
                    DayAndNight.Instance.OnDaySwitch += Disappear;
                    DayAndNight.Instance.OnNightSwitch += Appear;
                    if (DayAndNight.Instance.IsDaytime) Disappear();
                    break;
            }
        }

        private void Appear() {
            gameObject.SetActive(true);
        }

        private void Disappear() {
            gameObject.SetActive(false);
        }
    }
}
