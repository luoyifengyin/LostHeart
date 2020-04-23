﻿using MyGameApplication.Data;
using MyGameApplication.Data.Saver;
using MyGameApplication.Item;
using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MyGameApplication.Manager {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        [SerializeField] private string saveFileName = "save.archive";
        private PersistentSaveData gameData;
        private string saveFullPath;

        public event Action OnSaveSuccess;

        private void Awake() {
            DontDestroyOnLoad(transform.root.gameObject);
            Instance = this;
            saveFullPath = Application.persistentDataPath + "/" + saveFileName;
            gameData = PersistentSaveData.Instance;
        }

        //保存游戏（存档）
        public void SaveGame() {
            var savers = FindObjectsOfType<Saver>();
            foreach (var saver in savers) {
                if (saver.enabled) saver.Save();
            }
            SaveFile();
        }
        //把游戏数据保存到磁盘
        public async void SaveFile() {
            FileStream fs = new FileStream(saveFullPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            string json = JsonUtility.ToJson(gameData);
            await sw.WriteAsync(json);
            sw.Close();
            fs.Close();
            OnSaveSuccess?.Invoke();
            print("save success!");
        }

        //读取游戏存档
        public void LoadGame() {
            if (!HasSaveArchive()) return;
            FileStream fs = new FileStream(saveFullPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string json = sr.ReadToEnd();
            gameData = JsonUtility.FromJson<PersistentSaveData>(json);

        }

        //是否存在存档文件
        public bool HasSaveArchive() {
            return File.Exists(saveFullPath);
        }

        public void StartGame() {
            if (HasSaveArchive()) {
                // do something...
            }
            SceneController.LoadScene("CarRacing");
        }

        public void Exit() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
