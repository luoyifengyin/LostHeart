using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Item {
    public class CarItemBar : MonoBehaviour {
        private static CarItemBar _instance;

        private int m_ItemCapacity;
        private Image[] m_ItemImgs;
        private Queue<int> m_ItemQueue = new Queue<int>();
        private Sprite m_Mask;

        public static CarItemBar Instance {
            get {
                return _instance ?? (_instance = FindObjectOfType<CarItemBar>());
            }
        }

        private void Awake() {
            m_ItemCapacity = transform.childCount;
            m_ItemImgs = new Image[m_ItemCapacity];
            for (int i = 0; i < m_ItemCapacity; i++) {
                m_ItemImgs[i] = transform.GetChild(i).Find("Image").GetComponent<Image>();
            }
            m_Mask = m_ItemImgs[0].sprite;
        }

        public void AddProp(int id) {
            if (m_ItemQueue.Count < m_ItemCapacity) {
                int idx = m_ItemQueue.Count;
                m_ItemQueue.Enqueue(id);
                Sprite sprite = ItemManager.Instance.GetItemSpriteById(id);
                m_ItemImgs[idx].sprite = sprite;
            }
        }

        private void UseItem() {
            //print("use item");
            int id = m_ItemQueue.Dequeue();
            ItemManager.Instance.UseItemEffectById(id);
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
