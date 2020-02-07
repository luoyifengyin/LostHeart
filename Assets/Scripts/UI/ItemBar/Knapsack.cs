using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI.ItemBar {
    public class Knapsack : MonoBehaviour {
        [SerializeField] private GridLayoutGroup m_GridPanel = null;
        [SerializeField] private ItemDetail m_ItemDetail = null;
        private List<Grid> m_Grids = new List<Grid>();
        private int row, col;

        private void Awake() {
            m_Grids.AddRange(m_GridPanel.transform.GetComponentsInChildren<Grid>());
            gameObject.SetActive(false);
            col = m_GridPanel.constraintCount;
            row = 2;
        }

        public void Refresh() {
            if (!gameObject.activeSelf) return;
            var items = PlayerBag.Instance.GetItemsByType(ItemType.Prop);
            int j = 0;
            foreach(var item in items) {
                if (item.Value > 0) {
                    if (j > m_Grids.Count) {
                        for (int i = 0; i < col; i++) {
                            var gridObj = Instantiate(m_Grids[0].gameObject);
                            gridObj.transform.parent = m_GridPanel.transform;
                            Grid grid = gridObj.GetComponent<Grid>();
                            grid.detail = m_ItemDetail;
                            m_Grids.Add(grid);
                        }
                    }
                    if (!m_Grids[j].gameObject.activeSelf) m_Grids[j].gameObject.SetActive(true);
                    m_Grids[j++].SetItem(item.Key, item.Value);
                }
            }
            int bound = col * Mathf.CeilToInt((float)items.Count / col);
            if (bound < col * row) bound = col * row;
            while(j < m_Grids.Count) {
                if (j >= bound) m_Grids[j].gameObject.SetActive(false);
                m_Grids[j++].RemoveItem();
            }
            if (m_ItemDetail.ItemId <= 0) {
                if (m_Grids[0].ItemId > 0) m_Grids[0].OnSelected();
                else m_ItemDetail.Clear();
            }
        }

        private void OnEnable() {
            Refresh();
        }
    }
}
