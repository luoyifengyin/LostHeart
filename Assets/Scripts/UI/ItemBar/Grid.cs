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
        [HideInInspector] public Selectable selectable;

        public int ItemId { get; private set; }
        public ItemType ItemType { get; set; }

        private void Awake() {
            m_Img = transform.Find("Slot/Image").GetComponent<Image>();
            m_NameText = transform.Find("Name").GetComponent<Text>();
            m_CntText = transform.Find("Slot/Count").GetComponent<Text>();
            selectable = GetComponent<Selectable>();
        }

        public void SetItem(int id, ItemType type, Sprite sprite, string name, int cnt) {
            var itemManager = ItemManager.Instance;
            if (!sprite) sprite = itemManager.GetItemSprite(id, type);
            if (string.IsNullOrEmpty(name)) name = itemManager.GetItemName(id, type);
            if (cnt <= 0) cnt = PlayerBag.Instance.GetCntByIdAndType(id);
            ItemId = id;
            ItemType = type;
            m_Img.sprite = sprite;
            m_Img.enabled = true;
            m_NameText.text = name;
            m_CntText.text = cnt.ToString();
            selectable.interactable = true;
        }
        public void SetItem(int id, ItemType type, int cnt = 0) {
            SetItem(id, type, null, null, cnt);
        }

        public void RemoveItem() {
            ItemId = 0;
            m_Img.sprite = null;
            m_Img.enabled = false;
            m_NameText.text = "";
            m_CntText.text = "";
            selectable.interactable = false;
        }

        public void OnPointerDown(PointerEventData eventData) {
            print("pointer down");
            OnSelected();
        }

        public void OnSelected() {
            print("on selected");
            if (selectable && selectable.IsInteractable())
                detail.SetItem(ItemId, ItemType, m_Img.sprite, m_NameText.text);
        }
    }
}
