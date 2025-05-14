using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundShader : MonoBehaviour
{
    public Material backgroundShader;

    public Color defaultBGColor;
    public Color defaultNoiseColor;

    public Color passiveBGColorChange;
    public Color noiseBGColorChange;

    static public BackgroundShader instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        backgroundShader.SetColor("_ColorOverlay", defaultBGColor);
        backgroundShader.SetColor("_NoiseColor", defaultNoiseColor);
        backgroundShader.SetFloat("_Progression", 1f);
    }

    public IEnumerator PassiveSceneTransition(Color targetColor, Color targetNoiseColor, float targetProgression, bool transitionIn, float lerpSpeed = 1f)
    {
        for(float i = 0; i < 1; i += Time.fixedDeltaTime / lerpSpeed)
        {
            if(i + Time.fixedDeltaTime / lerpSpeed > 1)
            {
                backgroundShader.SetColor("_ColorOverlay", targetColor);
                backgroundShader.SetColor("_NoiseColor", targetNoiseColor);
                backgroundShader.SetFloat("_Progression", targetProgression);

                break;
            }

            backgroundShader.SetColor("_ColorOverlay", Color.Lerp(backgroundShader.GetColor("_ColorOverlay"), targetColor, i));
            backgroundShader.SetColor("_NoiseColor", Color.Lerp(backgroundShader.GetColor("_ColorOverlay"), targetNoiseColor, i));
            backgroundShader.SetFloat("_Progression", Mathf.Lerp(backgroundShader.GetFloat("_Progression"), targetProgression, i));

            yield return null;
        }

        backgroundShader.SetColor("_ColorOverlay", targetColor);
        backgroundShader.SetColor("_NoiseColor", targetNoiseColor);
        backgroundShader.SetFloat("_Progression", targetProgression);

        print("Im losing it chat");
    }
}
