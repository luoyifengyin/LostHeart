using MyGameApplication.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace MyGameApplication.TestTools {
    public static class DeveloperMode {
        [MenuItem("开发者模式/保存游戏", true)]
        [MenuItem("开发者模式/读取存档", true)]
        [MenuItem("开发者模式/切换场景", true)]
        static bool Check() {
            return EditorApplication.isPlaying;
        }

        [MenuItem("开发者模式/保存游戏")]
        public static void SaveGame() {
            GameManager.Instance.SaveGame();
        }

        [MenuItem("开发者模式/读取存档")]
        public static void LoadGame() {
            GameManager.Instance.LoadGame();
        }

        static readonly string saveFilePath = GameManager.SaveFullPath.Replace("/", "\\");
        [MenuItem("开发者模式/在资源管理器中打开存档")]
        public static void ShowSaveArchiveInExplorer() {
            UnityEngine.Debug.Log(saveFilePath);
            string args = string.Format("/Select, {0}", saveFilePath);
            ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
            Process.Start(pfi);
        }

        [MenuItem("开发者模式/删除PlayerPref键值")]
        public static void DeleteAllPrefs() {
            PlayerPrefs.DeleteAll();
            //string args = "计算机\\HKEY_CURRENT_USER\\SOFTWARE\\Unity\\UnityEditor\\DefaultCompany\\MiTuZhiXin";
            //ProcessStartInfo pfi = new ProcessStartInfo("regedit.exe", args);
            //Process.Start(pfi);
        }

        private static readonly string[] sceneNames = { "Origin", "CarRacing", "Second", "Maze" };
        private static int curSceneIndex = -1;
        [MenuItem("开发者模式/切换场景", false, 2000)]
        public static void SwitchScene() {
            if (curSceneIndex == -1) {
                curSceneIndex = Array.FindIndex(sceneNames,
                    str => str == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
            curSceneIndex = (curSceneIndex + 1) % sceneNames.Length;
            SceneController.LoadScene(sceneNames[curSceneIndex]);
        }


        //private void OnGUI() {
        //    if (GUI.Button(new Rect(0, 0, 100, 50), "保存游戏")) {
        //        SaveGame();
        //    } else if (GUI.Button(new Rect(0, 50, 100, 50), "读取存档")) {
        //        LoadGame();
        //    } else if (GUI.Button(new Rect(0, 100, 100, 50), "切换场景")) {
        //        SwitchScene();
        //    }
        //}
    }
}
