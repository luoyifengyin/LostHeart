using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Manager {
    public class SceneController : MonoBehaviour {
        private static SceneController _instance;
        //public Fader fader;
        //public float fadeDuration = 1f;
        public Animator transition;
        [SerializeField] private string startingSceneName = null;

        [HideInInspector] public event Action onBeforeSceneUnload;
        [HideInInspector] public event Action onAfterSceneLoad;

        private CustomYieldInstruction waitWhileFadingOut;
        private CustomYieldInstruction waitWhileFadingIn;

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
            //if (!fader) fader = FindObjectOfType<Fader>();
            waitWhileFadingOut = new WaitUntil(() => {
                AnimatorStateInfo info = transition.GetCurrentAnimatorStateInfo(0);
                //print("fadingOut: " + info.normalizedTime);
                return info.normalizedTime > 1.0f && info.IsName("FadeOut");
            });
            waitWhileFadingIn = new WaitUntil(() => {
                AnimatorStateInfo info = transition.GetCurrentAnimatorStateInfo(0);
                //print("fadingIn: " + info.normalizedTime);
                return info.normalizedTime > 1.0f && info.IsName("FadeIn");
            });
        }

        IEnumerator Start() {
            IsLoading = true;
            if (!string.IsNullOrEmpty(startingSceneName)) {
                //fader.Alpha = 1f;
                yield return StartCoroutine(LoadNextScene(startingSceneName.Trim()));
            }
            else {
                onAfterSceneLoad?.Invoke();
                yield return waitWhileFadingIn;
            }
            IsLoading = false;
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
            //yield return fader.Fade(1f, fadeDuration);
            transition.SetTrigger("FadeOut");
            yield return waitWhileFadingOut;

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
            //yield return fader.Fade(0f, fadeDuration);
            //yield return new WaitForSeconds(1);
            transition.SetTrigger("FadeIn");
            yield return waitWhileFadingIn;
        }
    }
}
