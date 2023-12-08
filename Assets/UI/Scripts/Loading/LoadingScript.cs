using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public GameObject loadingScreen;
    
    Slider progressBar;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        AudioListener.pause = false;
        progressBar = loadingScreen.GetComponentInChildren<Slider>();
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadAsyncScene(scene));
    }

    IEnumerator LoadAsyncScene(string scene)
    {
        // Turn on the loading screen.
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(/*targetValue*/1, /*duration*/1.0f));

        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!loadingOperation.isDone)
        {
            if (progressBar)
            {
                progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            }
            yield return null;
        }

        yield return StartCoroutine(FadeLoadingScreen(/*targetValue*/0, /*duration*/0.5f));
        loadingScreen.SetActive(false);
    }


    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        CanvasGroup canvasGroup = loadingScreen.GetComponent<CanvasGroup>();
        float startValue = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
}
