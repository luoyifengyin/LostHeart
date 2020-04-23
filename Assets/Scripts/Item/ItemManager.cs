using MyGameApplication.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGameApplication.Item {
    [Serializable]
    public class ItemManager : MonoBehaviour {
        [SerializeField] private ItemInfo m_ItemInfo;

        [HideInInspector] public ItemInfo.Param[] propList;

        private Dictionary<ItemType, BaseItem[]> m_Items =
            new Dictionary<ItemType, BaseItem[]>();

        public static ItemManager Instance { get; private set; }

        private void Awake() {
            Instance = this;
            Initialize();
        }

        private void Initialize() {
            Instance = this;
            propList = m_ItemInfo.sheets[0].list.ToArray();
            m_Items.Add(ItemType.Prop, Instance.propList);

            for(int i = 1;i < propList.Length; i++) {
                var prop = Instance.propList[i];
                prop.sprite = Resources.Load<Sprite>("Items/Props/Sprites/P" + i);
                prop.prefab = Resources.Load<GameObject>("Items/Props/Prefabs/P" + i);
                Type type = Type.GetType("MyGameApplication.Item.Effect.P" + i);
                if (type != null) {
                    prop.effect = Activator.CreateInstance(type) as PropEffect;
                }
            }
        }

        public string GetItemName(int id, ItemType type) {
            return m_Items[type][id].name;
        }
        public string GetItemDesc(int id, ItemType type) {
            return m_Items[type][id].description;
        }
        public int GetItemCapacity(int id, ItemType type) {
            return m_Items[type][id].capacity;
        }
        public Sprite GetItemSprite(int id, ItemType type) {
            return m_Items[type][id].sprite;
        }
        public GameObject GetItemPrefab(int id, ItemType type) {
            return m_Items[type][id].prefab;
        }

        public bool CanUsePropEffect(int id) {
            var effect = propList[id].effect;
            return effect != null && effect.Condition();
        }

        public void UsePropEffect(int id) {
            if (propList[id].effect != null) propList[id].effect.Payload();
        }
    }
}
