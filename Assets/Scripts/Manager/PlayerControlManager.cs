using MyGameApplication.Data.Saver;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Manager {
    public class PlayerControlManager : MonoBehaviour {
        public static PlayerControlManager Instance { get; private set; }
        public GameObject CurPlayer { get; private set; }

        [SerializeField] private List<GameObject> m_PlayerList = new List<GameObject>();
        [SerializeField] private List<Camera> m_CameraList = new List<Camera>();

        private Dictionary<int, PlayerExtraInfo> m_Players = new Dictionary<int, PlayerExtraInfo>();
        //public event Action OnPlayerControlSwitch;

        private class PlayerExtraInfo {
            public GameObject player;
            public Camera camera;
            public event Action onControlEnable;
            public event Action onControlDisable;
            public PlayerExtraInfo(GameObject player, Camera camera) {
                this.player = player;
                this.camera = camera;
            }
            public void OnEnable() {
                player.tag = "Player";
                if (camera) {
                    camera.gameObject.tag = "MainCamera";
                    camera.gameObject.SetActive(true);
                }
                if (onControlEnable == null && onControlDisable == null)
                    player.SetActive(true);
                onControlEnable?.Invoke();
            }
            public void OnDisable() {
                player.tag = "Untagged";
                if (camera) {
                    camera.gameObject.tag = "Untagged";
                    camera.gameObject.SetActive(false);
                }
                if (onControlEnable == null && onControlDisable == null)
                    player.SetActive(false);
                onControlDisable?.Invoke();
            }
        }

        private void Awake() {
            Instance = this;

            for(int i = 0;i < m_PlayerList.Count; i++) {
                m_Players.Add(m_PlayerList[i].GetInstanceID(), new PlayerExtraInfo(m_PlayerList[i], m_CameraList[i]));
            }
            gameObject.AddComponent<PlayerControlSaver>();
            CurPlayer = m_PlayerList[0];
        }

        private void Start() {
            for(int i = 1;i < m_PlayerList.Count; i++) {
                if (m_PlayerList[i] == CurPlayer) continue;
                m_Players[m_PlayerList[i].GetInstanceID()].OnDisable();
            }
        }

        public void SwitchPlayerControl(GameObject player) {
            if (player == CurPlayer) return;

            m_Players[CurPlayer.GetInstanceID()].OnDisable();
            //OnPlayerControlSwitch?.Invoke();
            m_Players[player.GetInstanceID()].OnEnable();

            CurPlayer = player;
        }
        public void SwitchPlayerControl(int instanceId) {
            var player = m_Players[instanceId].player;
            SwitchPlayerControl(player);
        }

        public void AddSwitchCallback(GameObject player, Action OnEnable, Action OnDisable) {
            var info = m_Players[player.GetInstanceID()];
            info.onControlEnable += OnEnable;
            info.onControlDisable += OnDisable;
            if (CurPlayer == player) OnEnable();
            else OnDisable();
        }
        public void RemoveSwitchCallback(GameObject player, Action OnEnable, Action OnDisable) {
            var info = m_Players[player.GetInstanceID()];
            info.onControlEnable -= OnEnable;
            info.onControlDisable -= OnDisable;
        }

        public int PlayerCnt => m_PlayerList.Count;
        public GameObject GetPlayer(int index) {
            return m_PlayerList[index];
        }
    }

    class PlayerControlSaver : Saver {
        protected override string CreateKey() {
            return SceneManager.GetActiveScene().name + "PlayerTag";
        }

        public override void Save() {
            gameData.Save(key, PlayerControlManager.Instance.CurPlayer.GetInstanceID());
        }

        public override void Load() {
            int instanceId = default;
            if (gameData.Load(key, ref instanceId)) {
                PlayerControlManager.Instance.SwitchPlayerControl(instanceId);
            }
        }
    }
}
