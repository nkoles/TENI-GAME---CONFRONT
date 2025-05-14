using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionShader : MonoBehaviour
{
    static public TransitionShader instance;

    public Material affirmationTransition;

    public Material _transitionShader;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(instance);
        }

        //_transitionShader = GetComponent<Image>().material;

        affirmationTransition.SetFloat("_DisolveFactor", -0.01f);
        _transitionShader.SetFloat("_DisolveFactor", 1);
    }

    private void Start()
    {
        StartCoroutine(SceneStart());
    }

    private IEnumerator SceneStart()
    {
        yield return StartCoroutine(Fade(true, 3f, _transitionShader));
        DialogueManager.instance.StartDialogue();
    }

    public void SetColor(Color color)
    {
        _transitionShader.SetColor("_DisolveColor", color);
    }

    public IEnumerator Fade(bool isFadingOut, float fadeTime, Material transitionMaterial)
    {
        float targetFade = 1;

        if (isFadingOut)
        {
            targetFade = -0.01f;
        }

        if(!isFadingOut)
        {
            //AudioManager.instance.PlaySFX("Transition");
            //AudioManager.instance.pitchDown = false;

            for (float i = 0; i < 1; i += Time.deltaTime / fadeTime)
            {
                //StartCoroutine(AudioManager.instance.FadeOut());
                transitionMaterial.SetFloat("_DisolveFactor", i);

                if (i + Time.deltaTime / fadeTime > 1)
                {
                    transitionMaterial.SetFloat("_DisolveFactor", 1);
                    break;
                }

                yield return null;
            }

            transitionMaterial.SetFloat("_DisolveFactor", 1);
        } else
        {
            //AudioManager.instance.PlaySFX("Transition");
            //StartCoroutine(AudioManager.instance.FadeOut());
            //AudioManager.instance.pitchDown = true;
            for (float i = 1; i > -0.01; i -= Time.deltaTime / fadeTime)
            {
                transitionMaterial.SetFloat("_DisolveFactor", i);

                if (i - Time.deltaTime / fadeTime < -0.01)
                {
                    transitionMaterial.SetFloat("_DisolveFactor", -0.01f);
                    break;
                }

                yield return null;
            }

            transitionMaterial.SetFloat("_DisolveFactor", -0.01f);
        }

            print("TransitionFade Complete");
    }

    //private IEnumerator TestTransitions()
    //{
    //    yield return Fade(false, 5f);

    //    yield return new WaitForSeconds(2f);

    //    yield return Fade(true, 5f);
    //}

    //private void Start()
    //{
    //    StartCoroutine(TestTransitions());
    //}
}
