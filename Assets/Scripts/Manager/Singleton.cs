using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication {
    public class Singleton<T> where T : new() {
        protected static T _instance;
        private static object s_Lock = new object();

        public static T Instance {
            get {
                if (_instance == null) {
                    lock (s_Lock) {
                        if (_instance == null) _instance = new T();
                    }
                }
                return _instance;
            }
        }

        public static void print(object message) {
            Debug.Log(message);
        }
    }
}
