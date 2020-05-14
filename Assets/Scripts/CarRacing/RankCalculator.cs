using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.CarRacing {
    [RequireComponent(typeof(Ranking))]
    public class RankCalculator : MonoBehaviour {
        private List<GameObject> m_RacerList = null;
        private GameObject m_PlayerCar = null;
        public Dictionary<GameObject, Order> racers = new Dictionary<GameObject, Order>();
        [HideInInspector] public Transform[] destinations = null;

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
            racers.Add(m_PlayerCar, new Order(0, default));
            for (int i = 1; i < m_RacerList.Count; i++) {
                int seg = m_RacerList[i].GetComponent<EnemyCar>().StartOrder;
                racers.Add(m_RacerList[i], new Order(seg, default));
            }
        }

        private void Update() {
            CalDistance();
            CalRank();
        }

        private void CalDistance() {
            foreach(var i in racers) {
                i.Value.distance = Vector3.Distance(i.Key.transform.position,
                    destinations[i.Value.segmentation].position);
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
            m_RacerList.Sort(Compare);
            m_Ranking.Refresh();
        }
        private int Compare(GameObject a, GameObject b) {
            return racers[a] < racers[b] ? -1 : 1;
        }
    }
}
