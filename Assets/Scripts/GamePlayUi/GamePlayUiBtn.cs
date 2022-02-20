using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GamePlayUiBtn : MonoBehaviour
{
    public Animator PausePanelAnim;
    //public Animator InventoryAnimator;
    //public GameObject ArrowUp;
    [SerializeField] AudioMixer mixer;
    //[SerializeField] GameObject bigArea;
    int openPanelTriggerId => Animator.StringToHash("OpenPanel");
    string menuSceneName = "Menu";

    [Header("Sound Sprite")]
    [SerializeField] Sprite SoundOff;
    [SerializeField] Sprite SoundOn;
    [SerializeField] GameObject SoundBtn;
    [SerializeField] Image SoundSprite;

    WaitForEndOfFrame waitToEndFrame = new WaitForEndOfFrame();

    [HideInInspector] public bool isPausePanelOpen;
    //[Header("Win Lose Part")]
    //[SerializeField] GameObject winLosePanel;
    //[SerializeField] GameObject homeBtn;
    //[SerializeField] GameObject retryBtn;
    //[SerializeField] GameObject LevelSelectBtn;
    //[SerializeField] Text winloseTxt;
    //[SerializeField] OpenSign openSign;
    //[SerializeField] AudioMixer mixer;

    //TODO : write win lose texts
    //[TextArea()]
    //[SerializeField] string loseTxt = "";
    //[TextArea()]
    //[SerializeField] string winTxt = "";







    private void Awake()
    {
        Time.timeScale = 1;

        //disable all winlose buttons
        //homeBtn.SetActive(false);
        //retryBtn.SetActive(false);
        //LevelSelectBtn.SetActive(false);
        //winLosePanel.SetActive(false);
    }

    private void Start()
    {
        mixer.SetFloat("AmbientVol", 0);
        mixer.SetFloat("MusicVol", 0);

        if (!PlayerPrefs.HasKey("Vfx"))
        {
            PlayerPrefs.SetFloat("Vfx", 1);
        }
        SetInitSound();
    }

    public void OpenPausePanel()
    {
        isPausePanelOpen = true;
        mixer.SetFloat("AmbientVol" , -80);
        mixer.SetFloat("MusicVol", -80);
        Time.timeScale = 0;
        PausePanelAnim.gameObject.SetActive(true);
        PausePanelAnim.SetBool(openPanelTriggerId, true);
        GetComponentInParent<Canvas>().sortingOrder = 200;
    }

    public void ClosePausePanel()
    {
        isPausePanelOpen = false;
        StartCoroutine(ClosePausePanelCoroutine());
    }

    IEnumerator ClosePausePanelCoroutine()
    {
        Time.timeScale = 1;
        mixer.SetFloat("AmbientVol", 0);
        mixer.SetFloat("MusicVol", 0);
        PausePanelAnim.SetBool(openPanelTriggerId, false);
        yield return waitToEndFrame;
        WaitForSecondsRealtime waitToPanelClose = new WaitForSecondsRealtime(PausePanelAnim.GetCurrentAnimatorStateInfo(0).length);
        yield return waitToPanelClose;
        GetComponentInParent<Canvas>().sortingOrder = 100;
        PausePanelAnim.gameObject.SetActive(false);
    }

    public void GoToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }

    //public void OpenInventoryPanel()
    //{
    //    AudioManager.AM_Instance.PlayArrowUp();
    //    InventoryAnimator.SetBool(openPanelTriggerId , true);
    //    ArrowUp.SetActive(false);
    //    bigArea.SetActive(true);
    //}

    //public void CloseInventoryPanel()
    //{
    //    InventoryAnimator.SetBool(openPanelTriggerId , false);
    //    ArrowUp.SetActive(true);
    //    bigArea.SetActive(false);
    //}
    
    public void ReloadScene()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
    }

    public void LevelSoundSwitcher()
    {
        if (PlayerPrefs.GetFloat("Vfx") == 1)
        {
            SoundSprite.sprite = SoundOff;
            mixer.SetFloat("MasterVol", -80);
            PlayerPrefs.SetFloat("Vfx", 0);
        }
        else if(PlayerPrefs.GetFloat("Vfx") == 0)
        {
            SoundSprite.sprite = SoundOn;
            mixer.SetFloat("MasterVol", 0);
            PlayerPrefs.SetFloat("Vfx", 1);
        }
    }

    void SetInitSound()
    {
        if(PlayerPrefs.GetFloat("Vfx") == 1)
        {
            SoundSprite.sprite = SoundOn;
            mixer.SetFloat("MasterVol", 0);
        }
        else if(PlayerPrefs.GetFloat("Vfx") == 0)
        {
            SoundSprite.sprite = SoundOff;
            mixer.SetFloat("MasterVol" , -80);
        }
    }



    //WIN LOSE PART
    //public void Win()
    //{
    //    StartCoroutine(WinCoroutine());
    //}

    //public void Lose()
    //{
    //    StartCoroutine(LoseCoroutine());
    //}

    //IEnumerator WinCoroutine()
    //{
    //    mixer.SetFloat("AmbientVol", -80);
    //    mixer.SetFloat("MusicVol", -80);
    //    openSign.OpenRelatedSignAndLevel();
    //    WaitForSecondsRealtime waitABit = new WaitForSecondsRealtime(0.5f);
    //    yield return waitABit;
    //    Time.timeScale = 0;
    //    winloseTxt.text = winTxt;
    //    homeBtn.SetActive(true);
    //    LevelSelectBtn.SetActive(true);
    //    winLosePanel.SetActive(true);
    //}

    //IEnumerator LoseCoroutine()
    //{
    //    mixer.SetFloat("AmbientVol", -80);
    //    mixer.SetFloat("MusicVol", -80);
    //    WaitForSecondsRealtime waitABit = new WaitForSecondsRealtime(0.5f);
    //    Time.timeScale = 0;
    //    yield return waitABit;
    //    winloseTxt.text = loseTxt;
    //    homeBtn.SetActive(true);
    //    retryBtn.SetActive(true);
    //    winLosePanel.SetActive(true);
    //}
    

    public void LoadLevelSelect()
    {
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextBuildIndex);
    }

}
