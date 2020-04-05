using MyGameApplication.Car;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.CarRacing {
    public class ReachGoal : MonoBehaviour {
        private static int s_Order = 0;

        [SerializeField] private int m_Order = 0;
        [SerializeField] private CarController m_Car = null;
        [SerializeField] private string m_PhaseName = null;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Car") && other.transform.FindGameObjectInParentWithTag("Player")) {
                if (s_Order < m_Order) {
                    s_Order = m_Order;
                    m_Car.gameObject.SetActive(true);
                    Ranking.Instance.SetPlayerName(m_PhaseName);
                }
                else if (m_Order < 0) {
                    SceneController.LoadScene("Second");
                }
            }
        }
    }
}
