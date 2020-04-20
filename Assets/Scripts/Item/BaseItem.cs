using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication.Item {
    public enum ItemType {
        Prop,
    }

    [Serializable]
    public abstract class BaseItem {
        //private static int s_Uid = 0;

        //public int Uid { get; private set; }    //道具的唯一标识id，所有道具都拥有不同的uid

        //private void Awake() {
        //    Uid = ++s_Uid;
        //}

        public int id;                              //道具id（必需）
        public ItemType type;                       //道具类型
        public string name;                         //道具名称（必需）
        public string description;                  //道具描述
        public int capacity = -1;                   //道具容量（-1代表无上限）
        [NonSerialized] public Sprite sprite;       //道具的精灵图，用于UI显示
        [NonSerialized] public GameObject prefab;
    }
}
