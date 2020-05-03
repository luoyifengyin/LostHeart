using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI.ItemBar {
    public class Knapsack : MonoBehaviour {
        [SerializeField] private GridLayoutGroup m_GridPanel = null;
        [SerializeField] private ItemDetail m_ItemDetail = null;
        private List<Cell> m_Cells = new List<Cell>();
        private int row, col;

        private bool m_RefreshFlag = false; //激活时是否需要刷新UI的标记，初始为false避免了OnEnable在Grid的Awake之前刷新UI

        private void Awake() {
            m_Cells.AddRange(m_GridPanel.GetComponentsInChildren<Cell>());
            col = m_GridPanel.constraintCount;
            row = 2;
        }

        private void Start() {
            //若角色背包的道具数量发生改变，则设置“下次激活时需要刷新”的标记
            PlayerBag.Instance.onItemChange += NeedToRefresh;
            m_RefreshFlag = true;
            //gameObject.SetActive(false);
        }

        private void NeedToRefresh() {
            m_RefreshFlag = true;
        }

        public void Refresh() {
            //根据PlayerBag的数据来显示道具UI
            var items = PlayerBag.Instance.GetItemsByType(ItemType.Prop);
            int j = 0;
            foreach(var item in items) {
                if (item.Value > 0) {
                    //如果道具格子不足，则新创建一行道具格子
                    if (j > m_Cells.Count) {
                        for (int i = 0; i < col; i++) {
                            var gridObj = Instantiate(m_Cells[0].gameObject);
                            gridObj.transform.parent = m_GridPanel.transform;
                            Cell cell = gridObj.GetComponent<Cell>();
                            cell.detail = m_ItemDetail;
                            m_Cells.Add(cell);
                        }
                    }
                    if (!m_Cells[j].gameObject.activeSelf) m_Cells[j].gameObject.SetActive(true);
                    m_Cells[j++].SetItem(item.Key, ItemType.Prop, item.Value);
                }
            }
            //把其他道具格子清空，并清除多余的格子
            int bound = col * Mathf.CeilToInt((float)items.Count / col);
            if (bound < col * row) bound = col * row;
            while(j < m_Cells.Count) {
                if (j >= bound) m_Cells[j].gameObject.SetActive(false);
                else if (!m_Cells[j].gameObject.activeSelf) m_Cells[j].gameObject.SetActive(true);
                m_Cells[j++].RemoveItem();
            }
            //默认选择第一个道具格子
            if (m_ItemDetail.ItemId <= 0) {
                if (m_Cells[0].ItemId > 0) {
                    m_Cells[0].selectable.Select();
                    //m_Cells[0].OnSelected();
                }
                else m_ItemDetail.Clear();
            }
            //刷新完毕，把标记置为false
            m_RefreshFlag = false;
        }

        private void LateUpdate() {
            if (gameObject.activeSelf && m_RefreshFlag) Refresh();
        }

        private void OnDestroy() {
            if (PlayerBag.Instance != null) PlayerBag.Instance.onItemChange -= NeedToRefresh;
        }
    }
}
