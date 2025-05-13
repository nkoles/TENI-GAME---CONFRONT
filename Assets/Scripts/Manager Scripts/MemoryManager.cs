using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;
    public Sprite gender;
    public GameplayManager gameplay;
    public float delay = 5.0f;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Choice(Sprite icon)
    {
        gender = icon;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameplay = GameplayManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameplay == null && gender != null)
        {
            StartCoroutine(Progress());
        }
    }

    public IEnumerator Progress()
    {
        yield return new WaitForSeconds(delay);

        gameplay = GameplayManager.Instance;
        gameplay.player.heart.GetComponent<HeartScript>().sr.sprite = gender;
    }
}
