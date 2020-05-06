using MyGameApplication.Data;
using MyGameApplication.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGameApplication.Maze.NPC.Reactions {
    //public enum ConditionType {
    //    Bool,
    //    Int,
    //    Float,
    //    String,
    //    Vector3,
    //    Quaternion,
    //    Prop,
    //}
    public enum ValueType {
        Const,
        Variable,
    }

    public class IfGotoReaction : Reaction {
        private static SaveData gameData;

        //public ConditionType conditionType;

        public string key;

        public ValueType valueType;
        public bool value;
        public string valueKey;

        //public bool boolValue;
        //public int intValue;
        //public float floatValue;
        //public string stringValue;
        //public Vector3 vector3Value;
        //public Quaternion quaternionValue;

        private Func<bool> compare;

        [SerializeField] private int gotoIdx;
        [SerializeField] private int elseGoto;

        private void OnEnable() {
            if (string.IsNullOrEmpty(key)) compare = () => true;
            else if (valueType == ValueType.Variable) {
                compare = () => {
                    //SaveData gameData = GameManager.Instance.GameData;
                    bool val = default;
                    if (gameData.Load(key, ref value) && gameData.Load(valueKey, ref val)) {
                        if (value == val) return true;
                    }
                    return false;
                };
            }
            else {
                compare = () => {
                    //SaveData gameData = GameManager.Instance.GameData;
                    bool val = default;
                    if (gameData.Load(key, ref val)) {
                        if (val == value) return true;
                    }
                    return false;
                };
            }
        }

        public override async Task React() {
            if (gameData == null) gameData = GameManager.Instance.GameData;
            await Task.Run(() => {
                if (compare()) reactionCollection.CurRunIndex = gotoIdx;
                else reactionCollection.CurRunIndex = elseGoto;
            });
        }
    }
}
