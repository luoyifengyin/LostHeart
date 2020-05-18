using MyGameApplication.Car;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.CarRacing {
    public class ActivateTrigger : MonoBehaviour {
        private static int s_Order = 0;

        [SerializeField] private int m_Order = 0;
        [SerializeField] private GameObject m_Car = null;
        [SerializeField] private string m_PhaseName = null;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Car") && other.transform.GetGameObjectInParentWithTag("Player")) {
                Ranking.Instance.SetPlayerName(m_PhaseName);

                if (s_Order < m_Order) {
                    s_Order = m_Order;
                    if (m_Car) {
                        m_Car.gameObject.SetActive(true);
                    }
                }
                else if (m_Order < 0) {
                    SceneController.LoadScene("Second");
                }
            }
        }
    }
}
