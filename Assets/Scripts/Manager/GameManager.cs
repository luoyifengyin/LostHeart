using MyGameApplication.Data;
using MyGameApplication.Data.Saver;
using MyGameApplication.Item;
using MyGameApplication.MainMenu;
using MyGameApplication.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Manager {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        [SerializeField] private string m_StartSceneName = "Origin";
        [SerializeField] private string m_SaveFileName = "save.archive";
        private PersistentSaveData gameData;
        private string m_SaveFullPath;

        public event Action OnSaveSuccess;

        private void Awake() {
            DontDestroyOnLoad(transform.root.gameObject);
            Instance = this;
            m_SaveFullPath = Application.persistentDataPath + "/" + m_SaveFileName;
            gameData = PersistentSaveData.Instance;
        }

        public void StartGame() {
            if (HasSaveArchive()) {
                // do something...
            }
            SceneController.LoadScene(m_StartSceneName);
        }

        public void Exit() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        //是否存在存档文件
        public bool HasSaveArchive() {
            return File.Exists(m_SaveFullPath);
        }

        //保存游戏（存档）
        public void SaveGame() {
            var savers = FindObjectsOfType<Saver>();
            foreach (var saver in savers) {
                if (saver.enabled) saver.Save();
            }
            Scene curScene = SceneManager.GetActiveScene();
            gameData.Save(curScene.GetType().FullName, curScene.name);
            SaveFile();
        }
        //把游戏数据保存到磁盘
        public async void SaveFile() {
            FileStream fs = new FileStream(m_SaveFullPath, FileMode.Create);
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
            FileStream fs = new FileStream(m_SaveFullPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string json = sr.ReadToEnd();
            gameData = JsonUtility.FromJson<PersistentSaveData>(json);

            Scene scene = default;
            string sceneName = default;
            if (gameData.Load(scene.GetType().FullName, ref sceneName)) {
                SceneController.LoadScene(sceneName);
            }
        }

        public bool IsPausing { get; private set; }
        private float m_TimeScaleRef = 1f;
        private float m_VolumeRef = 1f;
        [SerializeField] private bool m_StopSoundWhilePause;
        private WaitWhile m_WaitWhilePause;

        public void Pause() {
            m_TimeScaleRef = Time.timeScale;
            Time.timeScale = 0f;
            if (m_StopSoundWhilePause) {
                m_VolumeRef = AudioListener.volume;
                AudioListener.volume = 0f;
            }
            IsPausing = true;
            StartCoroutine(Pausing());
        }
        private IEnumerator Pausing() {
            Setting.Instance.OpenWindow();
            yield return null;
            yield return m_WaitWhilePause ?? (m_WaitWhilePause = new WaitWhile(() =>
                Setting.Instance.IsOpening && !CrossPlatformInputManager.GetButtonDown("Pause")));
            if (Setting.Instance.IsOpening) Setting.Instance.CloseWindow();
            Continue();
        }

        public void Continue() {
            Time.timeScale = m_TimeScaleRef;
            if (m_StopSoundWhilePause) {
                AudioListener.volume = m_VolumeRef;
            }
            IsPausing = false;
        }

        private void Update() {
            if (SceneManager.GetActiveScene().name != "MainMenu" &&
                CrossPlatformInputManager.GetButtonDown("Pause")) {
                if (!IsPausing) Pause();
                //else Continue();
            }
        }


        private void Start() {
            SceneController.Instance.onAfterSceneLoad += InitNewScene;
        }

        private void InitNewScene() {
            var list = FindObjectsOfType<Light>();
            foreach (var light in list) {
                if (light.cullingMask == -1) {
                    light.cullingMask ^= (1 << LayerMask.NameToLayer("PersistentUI"));
                }
            }
        }
    }
}
