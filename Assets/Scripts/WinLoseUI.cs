using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    //audio mixer ref
    [SerializeField] AudioMixer mixer;

    //for open level select part
    [SerializeField] OpenSign openSign;

    //child refrences
    [SerializeField] GameObject parent;
    [SerializeField] GameObject homeBtn;
    [SerializeField] GameObject nextBtn;
    [SerializeField] GameObject retryBtn;
    [SerializeField] GameObject middleHome;

    //sprite place
    [SerializeField] Image winLoseImagePlace;

    //win lose sprite
    [SerializeField] Sprite winSprite;
    [SerializeField] Sprite loseSprite;

    [SerializeField] GameObject rayBlocker;
    Azhir azhir;

    private void Awake()
    {
        azhir = FindObjectOfType<Azhir>();
    }

    private void Start()
    {
        parent.SetActive(false);
        homeBtn.SetActive(false);
        nextBtn.SetActive(false);
        retryBtn.SetActive(false);
        middleHome.SetActive(false);
    }

    public void Win()
    {
        AudioManager.AM_Instance.PlayWinSound();
        StartCoroutine(WinCoroutine());
    }

    public void Lose()
    {
        StartCoroutine(LoseCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        azhir.PauseAzhir();
        rayBlocker.SetActive(true);
        mixer.SetFloat("AmbientVol", -80);
        mixer.SetFloat("MusicVol", -80);
        openSign.OpenRelatedSignAndLevel();
        Time.timeScale = 0;
        WaitForSecondsRealtime waitABit = new WaitForSecondsRealtime(0.5f);
        yield return waitABit;
        if(SceneManager.GetActiveScene().name == "Level10")
        {
            middleHome.SetActive(true);
        }
        else
        {
            homeBtn.SetActive(true);
            nextBtn.SetActive(true);
        }        
        winLoseImagePlace.sprite = winSprite;
        parent.SetActive(true);
    }

    IEnumerator LoseCoroutine()
    {
        azhir.PauseAzhir();
        rayBlocker.SetActive(true);
        mixer.SetFloat("AmbientVol", -80);
        mixer.SetFloat("MusicVol", -80);
        Time.timeScale = 0;
        WaitForSecondsRealtime waitABit = new WaitForSecondsRealtime(0.5f);
        yield return waitABit;
        homeBtn.SetActive(true);
        retryBtn.SetActive(true);
        winLoseImagePlace.sprite = loseSprite;
        parent.SetActive(true);
    }
}
