using MyGameApplication.Car;
using MyGameApplication.Item;
using MyGameApplication.Item.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyGameApplication.PickUp {
    public class CarItemBox : BasePickUp {
        struct InfoBean {
            public int id;
            public int weight;
            public InfoBean(int id, int weight) {
                this.id = id;
                this.weight = weight;
            }
        }
        private static InfoBean[] m_CarRacingItemList = { new InfoBean(1, 10) };
        private static int[] m_WeightPreSum = null;

        [SerializeField] private float m_RefreshTime = 5;

        private void Start() {
            if (m_WeightPreSum == null || m_WeightPreSum.Length == 0) {
                int len = m_CarRacingItemList.Length;
                m_WeightPreSum = new int[len];
                int sum = 0;
                for (int i = 0; i < m_CarRacingItemList.Length; i++) {
                    sum += m_CarRacingItemList[i].weight;
                    m_WeightPreSum[i] = sum;
                }
            }
        }

        private int GetRandomCarItemId() {
            int rand = Random.Range(0, m_WeightPreSum[m_WeightPreSum.Length - 1]);
            int idx = Array.BinarySearch(m_WeightPreSum, rand + 1);
            if (idx < 0) idx = ~idx;
            return m_CarRacingItemList[idx].id;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Car")) {
                if (!other.gameObject.GetGameObjectInParentWithTag("Player")) {
                    OnPicked(0);
                    return;
                }
                int itemId = GetRandomCarItemId();

                if (ItemManager.Instance.propList[itemId].isCarItem)
                    m_Inventory = CarItemBar.Instance;
                else m_Inventory = PlayerBag.Instance;

                PickUpItem(itemId);
            }
        }

        protected override void OnPicked(int pickedCnt = 1, int overflowCnt = 0) {
            base.OnPicked(pickedCnt, overflowCnt);
            gameObject.SetActive(false);
            Invoke("Appear", m_RefreshTime);
        }
        private void Appear() {
            gameObject.SetActive(true);
        }
    }
}
