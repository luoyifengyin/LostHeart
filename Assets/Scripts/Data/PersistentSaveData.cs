using MyGameApplication.Item;
using MyGameApplication.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    public class PersistentSaveData : SaveData {
        [SerializeField] private List<int> propCntList = new List<int>();

        public void SavePropCnt(int itemId, int cnt) {
            while(propCntList.Count <= itemId) {
                propCntList.Add(0);
            }
            propCntList[itemId] = cnt;
        }

        public List<int> LoadPropCnt() {
            return propCntList;
        }
    }
}
