using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShader : MonoBehaviour
{
    static public BossShader instance;

    public Material bossShader;

    public Color outlineColor;
    public Color passiveOndulatingColor;

    public float defaultOutlineWidth;
    public float passiveOutlineBand;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        bossShader.SetFloat("_EdgeWidth", defaultOutlineWidth);
    }

    float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public void Update()
    {
        bossShader.SetFloat("_DecomposingFactor", Mathf.Lerp(-1, Remap(1 - (float)EnemyManager.instance.hp / (float)EnemyManager.instance.maxHP, 0, 1, -1, 1), Mathf.PingPong(Time.time/2, 1)));
    }

    public IEnumerator LerpOutline(float targetValue, float lerpSpeed)
    {
        for (float i = 0; i < 1; i += Time.fixedDeltaTime / lerpSpeed)
        {
            if(i + Time.fixedDeltaTime/lerpSpeed > 1)
            {
                bossShader.SetFloat("_EdgeWidth", targetValue);
            }

            bossShader.SetFloat("_EdgeWidth", Mathf.Lerp(bossShader.GetFloat("_EdgeWidth") , targetValue, i));

            yield return null;
        }
    }

    public IEnumerator LerpColor(float targetValue, float lerpSpeed)
    {
        for (float i = 0; i < 1; i += Time.fixedDeltaTime / lerpSpeed)
        {
            if (i + Time.fixedDeltaTime / lerpSpeed > 1)
            {
                bossShader.SetFloat("_EdgeWidth", targetValue);
            }

            bossShader.SetFloat("_EdgeWidth", Mathf.Lerp(bossShader.GetFloat("_EdgeWidth"), targetValue, i));

            yield return null;
        }
    }

    public IEnumerator LerpDissolve(float targetValue, float lerpSpeed)
    {
        for (float i = 0; i < 1; i += Time.fixedDeltaTime / lerpSpeed)
        {
            if (i + Time.fixedDeltaTime / lerpSpeed > 1)
            {
                bossShader.SetFloat("_DecomposingFactor", targetValue);
            }

            bossShader.SetFloat("_DecomposingFactor", Mathf.Lerp(bossShader.GetFloat("_DecomposingFactor"), targetValue, i));

            yield return null;
        }
    }

    public bool isOndulatingEdge;
    public bool isOndulatingColor;
    public bool isOndulatingDamage;
    public Coroutine runningRoutine;

    public IEnumerator OndulateEdge(float minValue, float maxValue, float speed, float time)
    {
        if (!isOndulatingEdge)
        {
            isOndulatingEdge = true;

            for (float i = 0; i < time; i += Time.deltaTime)
            {
                bossShader.SetFloat("_EdgeWidth", Mathf.PingPong(Time.time * speed, maxValue) + minValue);

                yield return null;
            }
        }

        isOndulatingEdge = false;
    }


    public IEnumerator OndulateDamage(float minValue, float maxValue, float speed, float time)
    {
        if (!isOndulatingDamage)
        {
            isOndulatingDamage = true;

            for (float i = 0; i < time; i += Time.deltaTime)
            {
                bossShader.SetFloat("_DecomposingFactor", Mathf.PingPong(Time.time * speed, maxValue) + minValue);

                yield return null;
            }
        }

        isOndulatingDamage = false;
    }
}
