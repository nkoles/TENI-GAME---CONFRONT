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

    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator SwitchScene(int buildIndex)
    {
        yield return TransitionShader.instance.Fade(false, 5, TransitionShader.instance._transitionShader);

        SceneManager.LoadScene(buildIndex);
    }
}
