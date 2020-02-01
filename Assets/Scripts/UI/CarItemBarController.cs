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
        private Queue<Prop> m_ItemQueue = new Queue<Prop>();
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

        public void addProp(Prop prop) {
            if (m_ItemQueue.Count < m_ItemCapacity) {
                int idx = m_ItemQueue.Count;
                m_ItemQueue.Enqueue(prop);
                m_ItemImgs[idx].sprite = ItemManager.Instance.GetSpriteById(prop.ItemId);
            }
        }

        private void FixedUpdate() {
            bool use = CrossPlatformInputManager.GetButtonDown("Fire1");
            if (use && m_ItemQueue.Count > 0) {
                Prop prop = m_ItemQueue.Dequeue();
                prop.PayLoad();
                for(int i = 0;i < m_ItemCapacity - 1; i++) {
                    m_ItemImgs[i].sprite = m_ItemImgs[i + 1].sprite;
                }
                m_ItemImgs[m_ItemCapacity - 1].sprite = m_Mask;
            }
        }
    }
}
