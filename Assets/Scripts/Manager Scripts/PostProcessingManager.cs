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

        ppCA = ScriptableObject.CreateInstance<ChromaticAberration>();
    }

    static public IEnumerator TakeDamagePPEffect(float startSpeed, float endSpeed, float holdTime)
    {
        print("entered");

        instance.ppCA.intensity.Override(1f);
        instance.ppCA.enabled.Override(true);

        instance.ppVolume = PostProcessManager.instance.QuickVolume(0 & 1 >> 5, 100, instance.ppCA);

        for(float i = 0; i < 1.1; i += startSpeed/Time.deltaTime)
        {
            instance.ppCA.intensity.Override(Mathf.Lerp(0, 1, i));

            yield return null;
        }

        instance.ppCA.intensity.Override(1f);

        yield return new WaitForSeconds(holdTime);

        for (float i = 0; i < 1.1; i += endSpeed/Time.deltaTime)
        {
            instance.ppCA.intensity.Override(Mathf.InverseLerp(1, 0, i));

            yield return null;
        }

        instance.ppCA.intensity.Override(0f);

        RuntimeUtilities.DestroyVolume(instance.ppVolume, true, false);
    }
}
