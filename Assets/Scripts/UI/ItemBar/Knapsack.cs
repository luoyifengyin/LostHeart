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

        private bool m_RefreshFlag = false; //激活时是否需要刷新UI的标记，初始为false避免了OnEnable在Grid的Awake之前刷新UI

        private void Awake() {
            m_Grids.AddRange(m_GridPanel.transform.GetComponentsInChildren<Grid>());
            col = m_GridPanel.constraintCount;
            row = 2;
        }

        private void Start() {
            m_RefreshFlag = true;
            gameObject.SetActive(false);
        }

        public void Refresh() {
            //如果没有激活，则设置“下次激活时需要刷新”的标记，并直接返回
            if (!gameObject.activeSelf) {
                m_RefreshFlag = true;
                return;
            }
            //根据PlayerBag的数据来显示道具UI
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
            //把其他道具格子清空，并清除多余的格子
            int bound = col * Mathf.CeilToInt((float)items.Count / col);
            if (bound < col * row) bound = col * row;
            while(j < m_Grids.Count) {
                if (j >= bound) m_Grids[j].gameObject.SetActive(false);
                m_Grids[j++].RemoveItem();
            }
            //默认选择第一个道具格子
            if (m_ItemDetail.ItemId <= 0) {
                if (m_Grids[0].ItemId > 0) m_Grids[0].OnSelected();
                else m_ItemDetail.Clear();
            }
            //刷新完毕，把标记置为false
            m_RefreshFlag = false;
        }

        private void OnEnable() {
            if (m_RefreshFlag) Refresh();
        }
    }
}
