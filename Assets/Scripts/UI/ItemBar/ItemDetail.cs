using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.UI.ItemBar {
    public class ItemDetail : MonoBehaviour {
        [SerializeField] private Image m_Img = null;
        [SerializeField] private Text m_NameText = null;
        [SerializeField] private Text m_DescText = null;

        public int ItemId { get; private set; }

        private void SetItem(Sprite sprite, string name, string desc) {
            if (m_Img) {
                m_Img.sprite = sprite;
                m_Img.enabled = true;
            }
            if (m_NameText) m_NameText.text = name;
            m_DescText.text = desc;
        }

        public void SetItem(int id, Sprite sprite, string name, string desc = null) {
            ItemId = id;
            var itemManager = ItemManager.Instance;
            if (string.IsNullOrEmpty(desc)) desc = itemManager.GetItemDescById(id);
            SetItem(sprite, name, desc);
        }

        public void Clear() {
            ItemId = 0;
            if (m_Img) {
                m_Img.sprite = null;
                m_Img.enabled = false;
            }
            if (m_NameText) m_NameText.text = "";
            m_DescText.text = "";
        }
    }
}
