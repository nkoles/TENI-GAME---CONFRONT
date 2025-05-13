using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    static public PostProcessingManager instance;

    public PostProcessVolume ppVolume;
    public ChromaticAberration ppCA;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public IEnumerator TakeDamagePPEffect(float startSpeed, float endSpeed, float holdTime)
    {
        AudioManager.instance.PlaySFX("Damage");

        print("entered");

        ppCA = ScriptableObject.CreateInstance<ChromaticAberration>();

        Vignette ppVG = ScriptableObject.CreateInstance<Vignette>();

        ppVG.intensity.Override(0f);
        ppVG.smoothness.Override(1f);
        ppVG.color.Override(Color.red);
        ppVG.rounded.Override(true);
        ppVG.enabled.Override(true);   

        ppCA.intensity.Override(0f);
        ppCA.enabled.Override(true);

        ppVolume = PostProcessManager.instance.QuickVolume(0 & 1 >> 5, 100, ppCA, ppVG);

        for(float i = 0; i < 1; i += Time.fixedDeltaTime/startSpeed)
        {

            if(i + Time.fixedDeltaTime / startSpeed > 1)
            {
                ppCA.intensity.Override(1f);
                ppVG.intensity.Override(0.5f);
                break;
            }

            ppCA.intensity.Override((float)Mathf.Lerp(0, 1, i));
            ppVG.intensity.Override((float)Mathf.Lerp(0, .5f, i));

            yield return null;
        }

        yield return null;
        //yield return new WaitForSeconds(holdTime);

        for (float j = 0; j < 1; j += Time.fixedDeltaTime/endSpeed)
        {

            if (j + Time.fixedDeltaTime / endSpeed> 1)
            {
                ppCA.intensity.Override(0f);
                ppVG.intensity.Override(0f);
                break;
            }

            ppCA.intensity.Override((float)Mathf.Lerp(1, 0, j));
            ppVG.intensity.Override((float)Mathf.Lerp(0.5f, 0, j));

            yield return null;
        }

        //RuntimeUtilities.DestroyVolume(ppVolume, false, false);
    }

    public void Test()
    {
        StartCoroutine(TakeDamagePPEffect(.25f, 3f, 0f));
    }
}
