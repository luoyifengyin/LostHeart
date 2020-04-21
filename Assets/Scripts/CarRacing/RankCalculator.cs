using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.CarRacing {
    [RequireComponent(typeof(Ranking))]
    public class RankCalculator : MonoBehaviour {
        private List<GameObject> m_RacerList = null;
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
            m_RacerList = m_Ranking.racerRankList;
        }

        private void Start() {
            m_PlayerCar = m_RacerList[0];
            for (int i = 0; i < m_RacerList.Count; i++) {
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

        //private void CalRank() {
        //    int cnt = 0;
        //    foreach(var i in racers) {
        //        if (i.Key == m_PlayerCar) continue;
        //        if (i.Value < racers[m_PlayerCar])
        //            cnt++;
        //    }
        //    if (m_Ranking.Rank != cnt + 1) m_Ranking.Rank = cnt + 1;
        //}
        private void CalRank() {
            m_RacerList.Sort((x, y) => {
                return racers[x] < racers[y] ? -1 : 1;
            });
            m_Ranking.Refresh();
        }
    }
}
