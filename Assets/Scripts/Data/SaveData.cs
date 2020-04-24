using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    [CreateAssetMenu(menuName = "SaveData/KeyValueListData")]
    public class SaveData : ScriptableObject {
        public List<string> boolKeys = new List<string>();
        public List<bool> boolValues = new List<bool>();

        public List<string> intKeys = new List<string>();
        public List<int> intValues = new List<int>();

        public List<string> floatKeys = new List<string>();
        public List<float> floatValues = new List<float>();

        public List<string> stringKeys = new List<string>();
        public List<string> stringValues = new List<string>();

        public List<string> vector3Keys = new List<string>();
        public List<Vector3> vector3Values = new List<Vector3>();

        public List<string> quaternionKeys = new List<string>();
        public List<Quaternion> quaternionValues = new List<Quaternion>();

        public void Clear() {
            boolKeys.Clear();
            boolValues.Clear();

            intKeys.Clear();
            intValues.Clear();

            floatKeys.Clear();
            floatValues.Clear();

            stringKeys.Clear();
            stringValues.Clear();

            vector3Keys.Clear();
            vector3Values.Clear();

            quaternionKeys.Clear();
            quaternionValues.Clear();
        }

        private void Save<T>(List<string> keyList, List<T> valueList, string key, T value) {
            int idx = keyList.FindIndex(x => x == key);
            if (idx >= 0) valueList[idx] = value;
            else {
                keyList.Add(key);
                valueList.Add(value);
            }
        }

        private bool Load<T>(List<string> keyList, List<T> valueList, string key, ref T value) {
            int idx = keyList.FindIndex(x => x == key);
            if (idx >= 0) {
                value = valueList[idx];
                return true;
            }
            return false;
        }

        public void Save(string key, bool value) {
            Save(boolKeys, boolValues, key, value);
        }

        public void Save(string key, int value) {
            Save(intKeys, intValues, key, value);
        }

        public void Save(string key, float value) {
            Save(floatKeys, floatValues, key, value);
        }

        public void Save(string key, string value) {
            Save(stringKeys, stringValues, key, value);
        }

        public void Save(string key, Vector3 value) {
            Save(vector3Keys, vector3Values, key, value);
        }

        public void Save(string key, Quaternion value) {
            Save(quaternionKeys, quaternionValues, key, value);
        }

        public bool Load(string key, ref bool value) {
            return Load(boolKeys, boolValues, key, ref value);
        }

        public bool Load(string key, ref int value) {
            return Load(intKeys, intValues, key, ref value);
        }

        public bool Load(string key, ref float value) {
            return Load(floatKeys, floatValues, key, ref value);
        }

        public bool Load(string key, ref string value) {
            return Load(stringKeys, stringValues, key, ref value);
        }

        public bool Load(string key, ref Vector3 value) {
            return Load(vector3Keys, vector3Values, key, ref value);
        }

        public bool Load(string key, ref Quaternion value) {
            return Load(quaternionKeys, quaternionValues, key, ref value);
        }
    }
}
