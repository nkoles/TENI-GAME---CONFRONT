using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    static public SceneLoadingManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);

        DontDestroyOnLoad(this);
    }

    public void LoadScene(int buildIndex)
    {
        StartCoroutine(SwitchScene(buildIndex));
    }

    public void openLink(string link)
    {
        Application.OpenURL(link);
    }

    public void closeScene()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
    }

    private IEnumerator SwitchScene(int buildIndex)
    {
        yield return TransitionShader.instance.Fade(false, 5, TransitionShader.instance._transitionShader);

        SceneManager.LoadScene(buildIndex);
    }
}
