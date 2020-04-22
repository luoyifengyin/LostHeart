using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameApplication {
    public class Singleton<T> {
        protected static T _instance;
        private static object s_Lock = new object();

        public static T Instance {
            get {
                if (_instance == null) {
                    lock (s_Lock) {
                        if (_instance == null) _instance = Activator.CreateInstance<T>();
                    }
                }
                return _instance;
            }
        }

        protected static void print(object message) {
            Debug.Log(message);
        }
    }
}
