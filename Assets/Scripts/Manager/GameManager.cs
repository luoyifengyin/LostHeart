using MyGameApplication.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MyGameApplication.Manager {
    public class GameManager : MonoBehaviour {
#if UNITY_EDITOR
        public static readonly bool _DEBUG_ = true;
#endif

        [SerializeField] private string saveFileName = "save.archive";
        public PersistentData gameData;

        private string saveFullPath;

        private void Awake() {
            DontDestroyOnLoad(gameObject);
            saveFullPath = Application.persistentDataPath + "/" + saveFileName;
        }

        public void SaveGame() {
            FileStream fs = new FileStream(saveFullPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            string json = JsonUtility.ToJson(gameData);
            sw.Write(json);
            sw.Close();
            fs.Close();
            print("save success!");
        }

        public void LoadGame() {
            if (!HasSaveArchive()) return;
            FileStream fs = new FileStream(saveFullPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string json = sr.ReadToEnd();
            gameData = JsonUtility.FromJson<PersistentData>(json);
        }

        public bool HasSaveArchive() {
            return Directory.Exists(saveFullPath);
        }

#if UNITY_EDITOR
        private void Update() {
            if (!_DEBUG_) return;
            if (Input.GetKeyDown(KeyCode.P)) {
                SceneController.LoadScene("Second");
            }
            else if (Input.GetKeyDown(KeyCode.O)) {
                SaveGame();
            }
        }
#endif
    }
}
