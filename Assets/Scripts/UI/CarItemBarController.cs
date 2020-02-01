using MyGameApplication.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.UI {
    public class CarItemBarController : MonoBehaviour {
        private static CarItemBarController _instance;

        private int m_ItemCapacity;
        private Image[] m_ItemImgs;
        private Queue<int> m_ItemQueue = new Queue<int>();
        private Sprite m_Mask;

        public static CarItemBarController Instance {
            get {
                return _instance ?? (_instance = FindObjectOfType<CarItemBarController>());
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

        public void addProp(int id) {
            if (m_ItemQueue.Count < m_ItemCapacity) {
                int idx = m_ItemQueue.Count;
                m_ItemQueue.Enqueue(id);
                Sprite sprite = ItemManager.Instance.GetSpriteById(id);
                m_ItemImgs[idx].sprite = sprite;
            }
        }

        private void useItem() {
            int id = m_ItemQueue.Dequeue();
            ItemManager.Instance.CreateItemObjectById(id).GetComponent<Prop>().PayLoad();
            for (int i = 0; i < m_ItemCapacity - 1; i++) {
                m_ItemImgs[i].sprite = m_ItemImgs[i + 1].sprite;
            }
            m_ItemImgs[m_ItemCapacity - 1].sprite = m_Mask;
        }

        private void FixedUpdate() {
            bool use = CrossPlatformInputManager.GetButtonDown("Fire1");
            if (use && m_ItemQueue.Count > 0) useItem();
        }
    }
}
