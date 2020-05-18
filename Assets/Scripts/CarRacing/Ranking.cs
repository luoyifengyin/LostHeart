using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.CarRacing {
    public class Ranking : MonoBehaviour {
        private static readonly string[] SUFFIXES = { "th", "st", "nd", "rd" };
        [SerializeField] private Text m_RankText = null;
        [SerializeField] private Text m_SuffixText = null;
        [SerializeField] private Text m_TotalText = null;
        [SerializeField] private GameObject m_RankingList = null;
        [SerializeField] private GameObject m_RankSlot = null;
        private Text[] m_Names;
        private Outline[] m_Outlines;
        [SerializeField] private string m_EnemyName = "Enemy Car";
        [SerializeField] private string m_PlayerName = "你";
        [SerializeField] private Color m_PlayerNameColor = default;
        [SerializeField] private Color m_EnemyNameColor = default;

        [HideInInspector] public List<GameObject> racerRankList = new List<GameObject>();
        private GameObject m_PlayerCar = null;

        public static Ranking Instance { get; private set; }

        public int Rank { get; private set; }

        private void Awake() {
            Instance = this;

            m_PlayerCar = GameObject.FindGameObjectWithTag("Player");
            racerRankList.Add(m_PlayerCar);
            racerRankList.AddRange(GameObject.FindGameObjectsWithTag("Racer"));
        }

        private void Start() {
            for(int i = racerRankList.Count; i < m_RankingList.transform.childCount; i++) {
                var go = m_RankingList.transform.GetChild(i).gameObject;
                go.SetActive(false);
                Destroy(go);
            }
            while(m_RankingList.transform.childCount < racerRankList.Count) {
                Instantiate(m_RankSlot, m_RankingList.transform);
            }

            m_Names = m_RankingList.GetComponentsInChildren<Text>();
            m_Outlines = m_RankingList.GetComponentsInChildren<Outline>();
            m_TotalText.text = "" + m_Names.Length;
            Rank = m_Names.Length;

            Refresh();
        }

        public void Refresh() {
            for (int i = 0; i < racerRankList.Count; i++) {
                if (racerRankList[i] == m_PlayerCar) {
                    if (Rank == i + 1) return;
                    else {
                        Rank = i + 1;
                        break;
                    }
                }
            }

            for (int i = 0; i < racerRankList.Count; i++) {
                if (i + 1 == Rank) {
                    m_Names[i].text = m_PlayerName;
                    m_Outlines[i].effectColor = m_PlayerNameColor;
                }
                else {
                    m_Names[i].text = m_EnemyName;
                    m_Outlines[i].effectColor = m_EnemyNameColor;
                }
            }
            m_RankText.text = "" + Rank;
            if (Rank <= 3) m_SuffixText.text = SUFFIXES[Rank];
            else m_SuffixText.text = SUFFIXES[0];
        }

        public GameObject GetRacer(int rank) {
            rank = Mathf.Clamp(rank, 1, racerRankList.Count);
            return racerRankList[rank - 1];
        }

        public void SetPlayerName(string name) {
            m_PlayerName = name;
            m_Names[Rank - 1].text = name;
        }
    }
}
