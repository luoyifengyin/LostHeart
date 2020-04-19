using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.CarRacing {
    [RequireComponent(typeof(Ranking))]
    public class RankCalculator : MonoBehaviour {
        private List<GameObject> m_RacerList = new List<GameObject>();
        private GameObject m_PlayerCar = null;
        public Dictionary<GameObject, Order> racers = new Dictionary<GameObject, Order>();
        //[SerializeField] private GameObject[] m_Goals;
        [SerializeField] private Transform[] m_Destinations = null;

        private Ranking m_Ranking;

        public class Order {
            public int segmentation;
            public float distance;
            public Order(int num, int dis) {
                segmentation = num;
                distance = dis;
            }
            public static bool operator < (Order a, Order b) {
                if (a.segmentation != b.segmentation)
                    return a.segmentation > b.segmentation;
                return a.distance < b.distance;
            }
            public static bool operator > (Order a, Order b) {
                return b < a;
            }
        }

        private void Awake() {
            m_Ranking = GetComponent<Ranking>();
            m_PlayerCar = GameObject.FindGameObjectWithTag("Player");
            m_RacerList.Add(m_PlayerCar);
            m_RacerList.AddRange(GameObject.FindGameObjectsWithTag("Racer"));
            for(int i = 0;i < m_RacerList.Count; i++) {
                racers.Add(m_RacerList[i], new Order(i, default));
            }
        }

        private void Update() {
            CalDistance();
            CalRank();
        }

        private void CalDistance() {
            foreach(var i in racers) {
                i.Value.distance = Vector3.Distance(i.Key.transform.position,
                    m_Destinations[i.Value.segmentation].position);
            }
        }

        private void CalRank() {
            int cnt = 0;
            foreach(var i in racers) {
                if (i.Key == m_PlayerCar) continue;
                if (i.Value < racers[m_PlayerCar])
                    cnt++;
            }
            if (m_Ranking.Rank != cnt + 1) m_Ranking.Rank = cnt + 1;
        }
    }
}
