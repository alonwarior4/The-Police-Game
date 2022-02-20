using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject TutorialPanel;
    public GameObject SelevtLevel;
    public GameObject ExitPanel;
    public GameObject AboutUsPanel;


    public void MainMenuclose()
    {
        if (MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(false);
        }

    }


    public void MainMenuOpen()
    {
        if (!MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(true);
        }
    }




    public void TutorialClose()
    {
        if (TutorialPanel.activeSelf)
        {
            TutorialPanel.SetActive(false);
        }
    }

    public void TutorialOpen()
    {
        if (!TutorialPanel.activeSelf)
        {
            TutorialPanel.SetActive(true);
        }
    }


    public void SelectLevelClose()
    {
        if (SelevtLevel.activeSelf)
            SelevtLevel.SetActive(false);
    }

    public void SelectLevelOpen()
    {
        if (!SelevtLevel.activeSelf)
            SelevtLevel.SetActive(true);
    }


    public void ClosePanel()
    {
        //if (ExitPanel.activeSelf)
        //{
        //    ExitPanel.SetActive(false);
        //}
        if (AboutUsPanel.activeSelf)
        {
            AboutUsPanel.SetActive(false);
        }
    }



  

}
