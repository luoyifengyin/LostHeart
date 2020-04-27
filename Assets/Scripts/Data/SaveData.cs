using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.Data {
    public class SaveData : ISerializationCallbackReceiver {
        [SerializeField] private List<string> boolKeys = new List<string>();
        [SerializeField] private List<bool> boolValues = new List<bool>();
        private Dictionary<string, bool> _boolDictionary = new Dictionary<string, bool>();

        [SerializeField] private List<string> intKeys = new List<string>();
        [SerializeField] private List<int> intValues = new List<int>();
        private Dictionary<string, int> _intDictionary = new Dictionary<string, int>();

        [SerializeField] private List<string> floatKeys = new List<string>();
        [SerializeField] private List<float> floatValues = new List<float>();
        private Dictionary<string, float> _floatDictionary = new Dictionary<string, float>();

        [SerializeField] private List<string> stringKeys = new List<string>();
        [SerializeField] private List<string> stringValues = new List<string>();
        private Dictionary<string, string> _stringDictionary = new Dictionary<string, string>();

        [SerializeField] private List<string> vector3Keys = new List<string>();
        [SerializeField] private List<Vector3> vector3Values = new List<Vector3>();
        private Dictionary<string, Vector3> _vector3Dictionary = new Dictionary<string, Vector3>();

        [SerializeField] private List<string> quaternionKeys = new List<string>();
        [SerializeField] private List<Quaternion> quaternionValues = new List<Quaternion>();
        private Dictionary<string, Quaternion> _quaternionDictionary = new Dictionary<string, Quaternion>();

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

        private void Save<T>(Dictionary<string, T> dic, string key, T value) {
            if (!dic.ContainsKey(key)) dic.Add(key, value);
            else dic[key] = value;
        }

        private bool Load<T>(Dictionary<string, T> dic, string key, ref T value) {
            if (dic.ContainsKey(key)) {
                value = dic[key];
                return true;
            }
            return false;
        }

        public void Save(string key, bool value) {
            Save(_boolDictionary, key, value);
        }

        public void Save(string key, int value) {
            Save(_intDictionary, key, value);
        }

        public void Save(string key, float value) {
            Save(_floatDictionary, key, value);
        }

        public void Save(string key, string value) {
            Save(_stringDictionary, key, value);
        }

        public void Save(string key, Vector3 value) {
            Save(_vector3Dictionary, key, value);
        }

        public void Save(string key, Quaternion value) {
            Save(_quaternionDictionary, key, value);
        }

        public bool Load(string key, ref bool value) {
            return Load(_boolDictionary, key, ref value);
        }

        public bool Load(string key, ref int value) {
            return Load(_intDictionary, key, ref value);
        }

        public bool Load(string key, ref float value) {
            return Load(_floatDictionary, key, ref value);
        }

        public bool Load(string key, ref string value) {
            return Load(_stringDictionary, key, ref value);
        }

        public bool Load(string key, ref Vector3 value) {
            return Load(_vector3Dictionary, key, ref value);
        }

        public bool Load(string key, ref Quaternion value) {
            return Load(_quaternionDictionary, key, ref value);
        }

        public void OnBeforeSerialize() {
            _boolDictionary.BeforeSerialize(boolKeys, boolValues);
            _intDictionary.BeforeSerialize(intKeys, intValues);
            _floatDictionary.BeforeSerialize(floatKeys, floatValues);
            _stringDictionary.BeforeSerialize(stringKeys, stringValues);
            _vector3Dictionary.BeforeSerialize(vector3Keys, vector3Values);
            _quaternionDictionary.BeforeSerialize(quaternionKeys, quaternionValues);
        }

        public void OnAfterDeserialize() {
            _boolDictionary.AfterDeserialize(boolKeys, boolValues);
            _intDictionary.AfterDeserialize(intKeys, intValues);
            _floatDictionary.AfterDeserialize(floatKeys, floatValues);
            _stringDictionary.AfterDeserialize(stringKeys, stringValues);
            _vector3Dictionary.AfterDeserialize(vector3Keys, vector3Values);
            _quaternionDictionary.AfterDeserialize(quaternionKeys, quaternionValues);
        }
    }
}
