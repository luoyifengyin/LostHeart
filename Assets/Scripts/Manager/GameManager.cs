using MyGameApplication.Data;
using MyGameApplication.Data.Saver;
using MyGameApplication.MainMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

namespace MyGameApplication.Manager {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        [SerializeField] private string m_StartSceneName = "Origin";

        private static string s_SaveFileName = "save.archive";
        public static string SaveFullPath { get; private set; }

        public PersistentSaveData GameData { get; private set; } = new PersistentSaveData();

        public event Action OnSaveSuccess;


        private void Awake() {
            if (Instance) {
                gameObject.SetActive(false);
                Destroy(transform.root.gameObject);
                return;
            }
            Instance = this;
            SaveFullPath = Application.persistentDataPath + "/" + s_SaveFileName;

            DontDestroyOnLoad(transform.root.gameObject);
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
            return File.Exists(SaveFullPath);
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
            Scene curScene = SceneManager.GetActiveScene();
            GameData.Save(curScene.GetType().FullName, curScene.name);

            string json = JsonUtility.ToJson(GameData);
#if UNITY_EDITOR
            await CreateFileAsync(Path.ChangeExtension(SaveFullPath, ".json"), json);
#endif
            await CreateFileAsync(SaveFullPath, Encrypt(json));
            OnSaveSuccess?.Invoke();
            print("save success!");
        }

        //读取游戏存档
        public async void LoadGame() {
            if (!HasSaveArchive()) return;
            string json = Decrypt(await LoadFileAsync(SaveFullPath));
            GameData = JsonUtility.FromJson<PersistentSaveData>(json);

            string sceneName = default;
            if (GameData.Load(typeof(Scene).FullName, ref sceneName)) {
                SceneController.LoadScene(sceneName);
            }
        }

        public async Task CreateFileAsync(string filePath, string text) {
            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            await sw.WriteAsync(text);
            sw.Close();
            fs.Close();
        }

        public async Task<string> LoadFileAsync(string filePath) {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string text = await sr.ReadToEndAsync();
            sr.Close();
            fs.Close();
            return text;
        }

        //加密与解密需要使用的32位密钥
        private readonly byte[] _secretKey = Encoding.UTF8.GetBytes("WuZhipengDengHuanlinChenXingming");
        
        public string Encrypt(string text) {        //加密
            ICryptoTransform encryptor = GetRijndaelManaged().CreateEncryptor();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            byte[] resArr = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            return Convert.ToBase64String(resArr);
        }

        public string Decrypt(string text) {        //解密
            ICryptoTransform decryptor = GetRijndaelManaged().CreateDecryptor();
            byte[] buffer = Convert.FromBase64String(text);
            byte[] resArr = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(resArr);
        }

        private RijndaelManaged GetRijndaelManaged() {
            RijndaelManaged rm = new RijndaelManaged {
                Key = _secretKey,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            return rm;
        }

        public bool IsPausing { get; private set; }
        private float m_TimeScaleRef = 1f;
        private float m_VolumeRef = 1f;
        [SerializeField] private bool m_StopSoundWhilePause = false;
        private WaitWhile m_WaitWhilePause;

        //暂停游戏
        public void Pause() {
            m_TimeScaleRef = Time.timeScale;
            Time.timeScale = 0f;
            if (m_StopSoundWhilePause) {
                m_VolumeRef = AudioListener.volume;
                AudioListener.volume = 0f;
            }
            IsPausing = true;
            GC.Collect();
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
