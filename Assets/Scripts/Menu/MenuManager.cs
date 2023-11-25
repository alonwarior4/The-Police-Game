using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;
    bool IsSettingOpen;

    //------------------------------------------BtnMenu------------------------------------
    [Header("SoundSprite")]
    public Sprite SoundBtnOn;
    public Sprite SoundBtnOff;
    public Sprite MusicBtnOn;
    public Sprite MusicBtnOff;
    public GameObject SoundBtn;
    public GameObject MusicBtn;


    //------------------------------------------PanelMenu------------------------------------
    [Header("MenuPanel")]
    public GameObject ExitPanel;
    public GameObject SelectLevelPanel;
    public GameObject MainMenuPanel;
    public Animator settingPanel;
    public GameObject TutorialPanel;
    public GameObject AboutUsPanel;
    public GameObject RestartPanel;

    //-----------------------------------------------------------------
    [Header("SettingBtn")]
    public Image soundBtnSprite;
    [SerializeField] Sprite soundOn;
    [SerializeField] Sprite soundOff;
    [SerializeField] AudioMixer mixer;

    SelectLevel selectLevel;
    Inventory inventory;



    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        selectLevel = FindObjectOfType<SelectLevel>();
        mixer.SetFloat("MusicVol", 0);
        mixer.SetFloat("AmbientVol", 0);

        if (PlayerPrefs.HasKey("FirstTime"))
        {
            //MusicChecker();
            //SoundChecker();
        }
        else
        {
            PlayerPrefs.SetString("FirstTime", "as");
        }

        if (!PlayerPrefs.HasKey("Vfx"))
        {
            PlayerPrefs.SetFloat("Vfx", 1);
        }

        SetInitSound();
    }

    void SetInitSound()
    {
        if (PlayerPrefs.GetFloat("Vfx") == 1)
        {
            soundBtnSprite.sprite = soundOn;
            mixer.SetFloat("MasterVol", 0);
        }
        else if (PlayerPrefs.GetFloat("Vfx") == 0)
        {
            soundBtnSprite.sprite = soundOff;
            mixer.SetFloat("MasterVol" , -80);
        }
    }



    public void settingPanelAnim()
    {
        IsSettingOpen = !IsSettingOpen;
        settingPanel.SetBool("OpenPanel", IsSettingOpen);
    } 



    public void ExitBtn()
    {
        if(!ExitPanel.activeSelf)
            ExitPanel.SetActive(true);

        ExitPanel.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenPanel",true);
    }



    public void PlayBtn()
    {
        //MainMenuPanel.SetActive(false);
        //SelectLevelPanel.SetActive(true);if

        SelectLevelPanel.GetComponent<Animator>().SetBool("OepnPanel",true);

        if (!SelectLevelPanel.activeSelf)
            SelectLevelPanel.SetActive(true);
    }


    public void restartAllGame()
    {

        //print("restart game!...");
        //delete save game...
        PlayerPrefs.DeleteAll();
        selectLevel.LoadLevel();
        inventory.LoadSignStatus();
        inventory.SetNotifOff();
    }


    bool OpenInventory;
    public void TutorialBtn(GameObject TutorialBtn)
    {
        OpenInventory = !OpenInventory;
        if (!TutorialPanel.activeSelf)
            TutorialPanel.SetActive(true);

        TutorialPanel.GetComponent<Animator>().SetBool("OpenPanel", OpenInventory);
        
    }



    public void AboutUsBtn()
    {
        if (!AboutUsPanel.activeSelf) {
            AboutUsPanel.SetActive(true);
        }
        AboutUsPanel.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenPanel", true);

    }

    public void restartPanel()
    {
        if (!RestartPanel.activeSelf)
        {
            RestartPanel.SetActive(true);
        }
        RestartPanel.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenPanel", true);

    }






    public void OtherApps()
    {
        Application.OpenURL("https://cgamestudio.ir/");
    }


    public void QuitApplication()
    {
        Application.Quit();
    }


    public void CloseAllPanel(GameObject PanelName)
    {
        IsSettingOpen = false;

        if (PanelName.name == "TutorialPanel")
        {
            OpenInventory = !OpenInventory;
            /*StartCoroutine(ToggleMainMenu());
            StartCoroutine(ToggleInventoryPanel());*/
            MainMenuPanel.SetActive(true);

            PanelName.GetComponent<Animator>().SetBool("OpenPanel", OpenInventory);
        }

        if (PanelName.name == "SelectLevel")
        {
            SelectLevelPanel.GetComponent<Animator>().SetBool("OepnPanel",false);
        }

        if (PanelName.name == "ExitPanel")
        {
            
            ExitPanel.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenPanel", false);
        }

        if (PanelName.name == "AboutUsPanel")
        {
            PanelName.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenPanel", false);
        }

        if (PanelName.name == "restartPanel")
        {
            PanelName.transform.GetChild(0).GetComponent<Animator>().SetBool("OpenPanel", false);
        }
        //PanelName.SetActive(false);
        /*if (!MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(true);
            // SettingBtn.GetComponent<Button>().interactable = true;
        }*/
    }


    //SelectLevelPanel.GetComponent<Animator>().SetBool("OepnPanel",true);



    //-------------------------------------------------------------------Sound
    public void MusicChangeSprite()
    {
        if (MusicBtn.GetComponent<Image>().sprite == MusicBtnOn)
        {
            SoundManager.instance.PlayMusic = false;
            MusicBtn.GetComponent<Image>().sprite = MusicBtnOff;
            SoundManager.instance.MusicChanger();
            PlayerPrefs.SetInt("MusicStatus", 0);
        }
        else
        {
            SoundManager.instance.PlayMusic = true;
            MusicBtn.GetComponent<Image>().sprite = MusicBtnOn;
            SoundManager.instance.MusicChanger();
            PlayerPrefs.SetInt("MusicStatus", 1);
        }
    }

    public void SoundChangeSprite()
    {
        if (SoundBtn.GetComponent<Image>().sprite == SoundBtnOn)
        {
            SoundManager.instance.sound = false;
            SoundBtn.GetComponent<Image>().sprite = SoundBtnOff;
            PlayerPrefs.SetInt("SoundStatus", 0);
        }
        else
        {
            SoundManager.instance.sound = true;
            SoundBtn.GetComponent<Image>().sprite = SoundBtnOn;
            PlayerPrefs.SetInt("SoundStatus", 1);
        }
    }

//------------------------------------------------------------------------SoundPlayerPref
    public void SoundChecker()
    {
        if (PlayerPrefs.HasKey("SoundStatus"))
        {
            if (PlayerPrefs.GetInt("SoundStatus") == 0)
            {
                SoundManager.instance.sound = false;
                SoundBtn.GetComponent<Image>().sprite = SoundBtnOff;
                SoundManager.instance.MusicChanger();
            }
            if (PlayerPrefs.GetInt("SoundStatus") == 1)
            {
                SoundManager.instance.sound = true;
                SoundBtn.GetComponent<Image>().sprite = SoundBtnOn;
                SoundManager.instance.MusicChanger();
            }
        }
    }

    public void MusicChecker()
    {
        if (PlayerPrefs.HasKey("MusicStatus"))
        {
            if (PlayerPrefs.GetInt("MusicStatus") == 0)
            {
                SoundManager.instance.PlayMusic = false;
                MusicBtn.GetComponent<Image>().sprite = MusicBtnOff;
                SoundManager.instance.MusicChanger();
            }
            if (PlayerPrefs.GetInt("MusicStatus") == 1)
            {
                SoundManager.instance.PlayMusic = true;
                MusicBtn.GetComponent<Image>().sprite = MusicBtnOn;
                SoundManager.instance.MusicChanger();
            }
        }
    }

    //*******************************

    public void LevelSoundSwitcher()
    {
        if (PlayerPrefs.GetFloat("Vfx") == 1)
        {
            soundBtnSprite.sprite = soundOff;
            mixer.SetFloat("MasterVol", -80);
            PlayerPrefs.SetFloat("Vfx", 0);
        }
        else if (PlayerPrefs.GetFloat("Vfx") == 0)
        {
            soundBtnSprite.sprite = soundOn;
            mixer.SetFloat("MasterVol", 0);
            PlayerPrefs.SetFloat("Vfx", 1);
        }
    }

}
