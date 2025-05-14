using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    static public SceneLoadingManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayMusic(string music)
    {
        AudioManager.instance.PlayMusic("music");
    }

    public void LoadScene(int buildIndex)
    {
        if(buildIndex == 0)
        {
            AudioManager.instance.principalBadEnd = false;
            AudioManager.instance.auntBadEnd = false;
            AudioManager.instance.doctorBadEnd = false;
            AudioManager.instance.battleEnd = false;
            AudioManager.instance.therapistEnd = false;
            //AudioManager.instance.fadeIn = true;
            AudioManager.instance.mainTheme = true;

            AudioManager.instance.PlayMusic("Main Theme");
            AudioManager.instance.mainTheme = false;
        }
        if(buildIndex == 5)
        {
            AudioManager.instance.principalBadEnd = false;
            AudioManager.instance.auntBadEnd = false;
            AudioManager.instance.doctorBadEnd = false;
            AudioManager.instance.battleEnd = false;
            AudioManager.instance.therapistEnd = true;
            //AudioManager.instance.fadeIn = true;
        }
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

    static private IEnumerator SwitchScene(int buildIndex)
    {
        yield return TransitionShader.instance.Fade(false, 5, TransitionShader.instance._transitionShader);

        SceneManager.LoadScene(buildIndex);
    }
}
