using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MyGameApplication.UI.ItemBar {
    public class Grid : MonoBehaviour, IPointerDownHandler {
        private Image m_Img;
        private Text m_NameText;
        private Text m_CntText;
        public ItemDetail detail = null;

        public int ItemId { get; private set; }

        private void Awake() {
            m_Img = transform.Find("Slot/Image").GetComponent<Image>();
            m_NameText = transform.Find("Name").GetComponent<Text>();
            m_CntText = transform.Find("Slot/Count").GetComponent<Text>();
        }

        public void SetItem(int id, Sprite sprite = null, string name = null, int cnt = 0) {
            var itemManager = ItemManager.Instance;
            if (!sprite) sprite = itemManager.GetItemSpriteById(id);
            if (string.IsNullOrEmpty(name)) name = itemManager.GetItemNameById(id);
            if (cnt <= 0) cnt = PlayerBag.Instance.GetCntById(id);
            ItemId = id;
            m_Img.sprite = sprite;
            m_Img.enabled = true;
            m_NameText.text = name;
            m_CntText.text = cnt.ToString();
        }
        public void SetItem(int id, int cnt = 0) {
            SetItem(id, null, null, cnt);
        }

        public void RemoveItem() {
            ItemId = 0;
            m_Img.sprite = null;
            m_Img.enabled = false;
            m_NameText.text = "";
            m_CntText.text = "";
        }

        public void OnPointerDown(PointerEventData eventData) {
            print("pointer down");
            OnSelected();
        }

        public void OnSelected() {
            print("on selected");
            if (ItemId > 0) {
                detail.SetItem(ItemId, m_Img.sprite, m_NameText.text);
            }
        }
    }
}
