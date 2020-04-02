using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.CarRacing {
    public class Ranking : MonoBehaviour {
        private static Ranking _instance;
        private static readonly string[] SUFFIXES = { "th", "st", "nd", "rd" };
        [SerializeField] private Text m_RankText = null;
        [SerializeField] private Text m_SuffixText = null;
        [SerializeField] private Text m_TotalText = null;
        [SerializeField] private GameObject m_RankingList = null;
        private Text[] m_Names;
        private Outline[] m_Outlines;
        private int m_Rank;
        [SerializeField] private string m_EnemyName = "Enemy Car";
        [SerializeField] private string m_PlayerName = "你";
        [SerializeField] private Color m_PlayerNameColor = default;
        [SerializeField] private Color m_EnemyNameColor = default;

        public static Ranking Instance {
            get { return _instance ?? (_instance = FindObjectOfType<Ranking>()); }
        }

        public int Rank {
            get { return m_Rank; }
            set {
                m_Rank = value;
                Refresh();
            }
        }

        private void Awake() {
            m_Names = m_RankingList.GetComponentsInChildren<Text>();
            m_Outlines = m_RankingList.GetComponentsInChildren<Outline>();
            m_TotalText.text = "" + m_Names.Length;
            Rank = m_Names.Length;
        }

        private void Refresh() {
            for(int i = 0;i < m_Names.Length; i++) {
                if (i + 1 == m_Rank) {
                    m_Names[i].text = m_PlayerName;
                    m_Outlines[i].effectColor = m_PlayerNameColor;
                }
                else {
                    m_Names[i].text = m_EnemyName;
                    m_Outlines[i].effectColor = m_EnemyNameColor;
                }
            }
            m_RankText.text = "" + m_Rank;
            if (m_Rank <= 3) m_SuffixText.text = SUFFIXES[m_Rank];
            else m_SuffixText.text = SUFFIXES[0];
        }

        public void SetPlayerName(string name) {
            m_PlayerName = name;
            m_Names[m_Rank].text = name;
        }
    }
}
