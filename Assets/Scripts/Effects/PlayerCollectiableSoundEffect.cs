using System;
using System.Collections;
using UnityEngine;

public class PlayerCollectiableSoundEffect : MonoBehaviour {

    [FMODUnity.EventRef]
    public String GemCollectionSoundEffect = "";
    private float GemsCollectedParameter = 0f;
    [SerializeField] private float PitchResetDelay;
    [SerializeField] private float PitchResetLenghtGemsPerSecond;
    [SerializeField] private AnimationCurve FadeCurve;

    public void GemCollected()
    {
        GemsCollectedParameter++;
        var sound = FMODUnity.RuntimeManager.CreateInstance(GemCollectionSoundEffect);
        sound.setParameterValue("CollectedPickUps", GemsCollectedParameter);
        sound.start();
        sound.release();

        StopAllCoroutines();
        StartCoroutine(PitchFade());
    }

    private IEnumerator PitchFade()
    {
        var t = 0f;

        while (t <= PitchResetDelay)
        {
            t += Time.deltaTime;
            yield return null;
        }

        t = 0f;
        var startGems = GemsCollectedParameter;

        while (t <= PitchResetDelay)
        {
            t += Time.deltaTime;
            GemsCollectedParameter = Mathf.Lerp(startGems, 0, FadeCurve.Evaluate(t / PitchResetDelay));
            yield return null;
        }
    }
}
