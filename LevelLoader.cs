using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen; // the UI obj that the whole level loading screen is on
    [SerializeField] Slider loadingBar; // the slider used to represent a loading bar
    [SerializeField] TextMeshProUGUI loadingText; // the text element that is used to show loading %
    [SerializeField] GameObject pressAnyKey; // the UI obj that tell the player they can press any button to continue

    private void Start()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadLevel(int index)
    {
        StartCoroutine(LoadAsynchronously(index));
    }
    IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        pressAnyKey.SetActive(false);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            loadingText.text = progress * 100f + "%";

            if (progress == 1)
                pressAnyKey.SetActive(true);

            if (Input.anyKeyDown)
                operation.allowSceneActivation = true;
            yield return null;
        }



    }
}
