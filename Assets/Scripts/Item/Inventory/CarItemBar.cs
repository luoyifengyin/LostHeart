using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Item.Inventory {
    public class CarItemBar : MonoBehaviour, IInventory {
        public static CarItemBar Instance { get; private set; }

        private int m_ItemCapacity;
        private Image[] m_ItemImgs;
        private Queue<int> m_ItemQueue = new Queue<int>();
        private Sprite m_Mask;

        private void Awake() {
            Instance = this;
            m_ItemCapacity = transform.childCount;
            m_ItemImgs = new Image[m_ItemCapacity];
            for (int i = 0; i < m_ItemCapacity; i++) {
                m_ItemImgs[i] = transform.GetChild(i).Find("Image").GetComponent<Image>();
            }
            m_Mask = m_ItemImgs[0].sprite;
        }

        public int AddItem(int id, ItemType type = ItemType.Prop, int cnt = 1) {
            return AddProp(id);
        }
        public int AddProp(int id) {
            if (m_ItemQueue.Count < m_ItemCapacity) {
                int idx = m_ItemQueue.Count;
                m_ItemQueue.Enqueue(id);
                Sprite sprite = ItemManager.Instance.GetItemSprite(id, ItemType.Prop);
                m_ItemImgs[idx].sprite = sprite;
                return 1;
            }
            return 0;
        }

        private void UseItem() {
            //print("use item");
            int id = m_ItemQueue.Dequeue();
            ItemManager.Instance.UsePropEffect(id);
            for (int i = 0; i < m_ItemCapacity - 1; i++) {
                m_ItemImgs[i].sprite = m_ItemImgs[i + 1].sprite;
            }
            m_ItemImgs[m_ItemCapacity - 1].sprite = m_Mask;
        }

        private void Update() {
            bool use = CrossPlatformInputManager.GetButtonDown("Fire1");
            if (use && m_ItemQueue.Count > 0) UseItem();
        }
    }
}
