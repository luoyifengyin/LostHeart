using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Texture texture;
    public float fadeDuration;
    [SerializeField] private string m_StartingSceneName = "CarRacing";

    private Rect rect;
    private bool isFading;

    [HideInInspector] public Action beforeSceneUnload;
    [HideInInspector] public Action afterSceneLoad;

    private void SetAlpha(float alpha) {
        Color color = GUI.color;
        GUI.color = new Color(color.r, color.g, color.b, alpha);
    }

    private void Awake() {
        rect = new Rect(0, 0, Screen.width, Screen.height);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        SetAlpha(1f);
        SceneManager.LoadSceneAsync(m_StartingSceneName, LoadSceneMode.Additive);
        yield break;
    }

    private IEnumerator Loading(string sceneName) {
        yield return StartCoroutine(Fade(1f));
        beforeSceneUnload?.Invoke();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        yield return StartCoroutine(LoadScene(sceneName));
        afterSceneLoad?.Invoke();
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator LoadScene(string sceneName) {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }

    private IEnumerator Fade(float finalAlpha) {
        isFading = true;

        float speed = Mathf.Abs(GUI.color.a - finalAlpha) / fadeDuration;
        while(!Mathf.Approximately(GUI.color.a, finalAlpha)) {
            SetAlpha(Mathf.MoveTowards(GUI.color.a, finalAlpha, speed * Time.deltaTime));
            yield return null;
        }

        isFading = false;
    }
}
