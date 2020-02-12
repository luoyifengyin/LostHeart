﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Manager {
    public class SceneController : MonoBehaviour {
        private static SceneController _instance;
        public Fader fader;
        public float fadeDuration = 1f;
        [SerializeField] private string startingSceneName = null;

        [HideInInspector] public event Action onBeforeSceneUnload;
        [HideInInspector] public event Action onAfterSceneLoad;

        public bool IsLoadedByPersistentScene {
            get { return !string.IsNullOrEmpty(startingSceneName); }
        }
        public bool IsLoading { get; private set; }

        public static SceneController Instance {
            get {
                return _instance ?? (_instance = FindObjectOfType<SceneController>());
            }
        }

        private void Awake() {
            if (!fader) fader = FindObjectOfType<Fader>();
        }

        IEnumerator Start() {
            if (!string.IsNullOrEmpty(startingSceneName)) {
                IsLoading = true;
                fader.Alpha = 1f;
                yield return StartCoroutine(LoadNextScene(startingSceneName.Trim()));
                IsLoading = false;
            }
            else onAfterSceneLoad?.Invoke();
        }

        public static void LoadScene(string sceneName) {
            Instance.SwitchScene(sceneName);
        }

        public void SwitchScene(string sceneName) {
            if (!IsLoading) StartCoroutine(Loading(sceneName));
        }

        private IEnumerator Loading(string sceneName) {
            IsLoading = true;
            yield return StartCoroutine(UnloadCurrentScene());
            yield return StartCoroutine(LoadNextScene(sceneName));
            IsLoading = false;
        }

        private IEnumerator UnloadCurrentScene() {
            yield return fader.Fade(1f, fadeDuration);
            onBeforeSceneUnload?.Invoke();
            if (IsLoadedByPersistentScene)
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        private IEnumerator LoadNextScene(string sceneName) {
            LoadSceneMode loadSceneMode = LoadSceneMode.Single;
            if (IsLoadedByPersistentScene) loadSceneMode = LoadSceneMode.Additive;
            yield return SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
            onAfterSceneLoad?.Invoke();
            yield return fader.Fade(0f, fadeDuration);
        }
    }
}
