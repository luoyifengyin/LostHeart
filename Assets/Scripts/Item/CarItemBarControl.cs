using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Item {
    public class CarItemBarControl : MonoBehaviour {
        private static CarItemBarControl _instance;

        private int m_ItemCapacity;
        private Image[] m_ItemImgs;
        private Queue<int> m_ItemQueue = new Queue<int>();
        private Sprite m_Mask;

        public static CarItemBarControl Instance {
            get {
                return _instance ?? (_instance = FindObjectOfType<CarItemBarControl>());
            }
        }

        private void Awake() {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("ItemSprite");
            m_ItemCapacity = gos.Length;
            m_ItemImgs = new Image[m_ItemCapacity];
            for (int i = 0; i < gos.Length; i++) {
                m_ItemImgs[i] = gos[i].GetComponent<Image>();
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
            bool use = Input.GetButtonDown("Fire1");
            if (use && m_ItemQueue.Count > 0) UseItem();
        }
    }
}
