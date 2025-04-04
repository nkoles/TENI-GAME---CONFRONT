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
        SceneManager.LoadScene(buildIndex);
    }

    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
