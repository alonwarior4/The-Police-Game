using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AzanManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource azanAS;
    [SerializeField] float azanStartDelay; 
    [SerializeField] float transitionToMusicVol;
    [SerializeField] float transitionToAzanVol;
    [SerializeField] float azanPlayTime;
    WaitForEndOfFrame waitToEndFrame = new WaitForEndOfFrame();



    IEnumerator Start()
    {
        WaitForSecondsRealtime waitToStartAzan = new WaitForSecondsRealtime(azanStartDelay);
        WaitForSecondsRealtime waitToEndAzan = new WaitForSecondsRealtime(azanPlayTime);
        yield return waitToStartAzan;
        azanAS.Play();
        yield return StartCoroutine(TransitionMusicToAzan());
        yield return waitToEndAzan;
        yield return StartCoroutine(TransitionAzanToMusic());
    }

    IEnumerator TransitionMusicToAzan()
    {
        float musicVol = 0;
        float azanVol = -80;
        for (float f = 0; f < transitionToAzanVol + 0.5f; f += Time.unscaledDeltaTime)
        {
            float currentTimeScale = Time.timeScale;
            Time.timeScale = 1;
            musicVol = Mathf.Lerp(0, -20, Mathf.Min(1, (f / transitionToAzanVol)));
            azanVol = Mathf.Lerp(-80, 0, Mathf.Min(1, (f / transitionToAzanVol)));
            mixer.SetFloat("MusicVol", musicVol);
            mixer.SetFloat("AzanVol", azanVol);
            Time.timeScale = currentTimeScale;
            yield return waitToEndFrame;
        }
    }

    IEnumerator TransitionAzanToMusic()
    {
        float musicVol = -20;
        float azanVol = 0;
        for (float k = 0; k < transitionToMusicVol + 0.5f; k += Time.unscaledDeltaTime)
        {
            float currentTimeScale = Time.timeScale;
            Time.timeScale = 1;
            musicVol = Mathf.Lerp(-20 , 0, Mathf.Min(1, (k / transitionToMusicVol)));
            azanVol = Mathf.Lerp(0 , -80, Mathf.Min(1, (k / transitionToMusicVol)));            
            mixer.SetFloat("MusicVol", musicVol);
            mixer.SetFloat("AzanVol", azanVol);
            Time.timeScale = currentTimeScale;
            yield return waitToEndFrame;
        }
    }

}
