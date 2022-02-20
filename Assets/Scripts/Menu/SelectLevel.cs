using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;



public class SelectLevel : MonoBehaviour
{

    public bool LevelOpened;
    public GameObject LockLevelText;
    public List<GameObject>  Buttons;
    //public GameObject ParentLevel;
    public Sprite[] Unlock;
    public Sprite lockSp;

    //list of Levels
    public Transform[] levels;



    private void Awake()
    {

        //PlayerPrefs.DeleteAll();
        LoadLevel();

    }
    


    public void selectLevels(GameObject LevelBtn)
    {
        if (LevelBtn.tag == "Open")
        {
            OpenLevel(LevelBtn.name);
        }
        else
        {
            LockLevelText.SetActive(true);
            StartCoroutine("closeLockText"); 
        }
    }
    


    public void OpenLevel(String levelName)
    {
        if (levelName == "Level1")
        {
            SceneManager.LoadScene("Level1_S");
        }
        else
        {
            //print(levelName);
            SceneManager.LoadScene(levelName);
        }
    }
    
    

    IEnumerator closeLockText()
    {
        yield return new WaitForSeconds(2f);
        LockLevelText.SetActive(false);
    }



    [Header("TestLevelInventory")]
    public int NumberStage;
    public int NumberSign;

    //TODO : for test save and load method
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        print("SomeLevel is Open");
    //        stageIsOver(NumberStage, NumberSign);
    //        LoadLevel();
    //    }
    //}



    public static void stageIsOver(int LevelNumber,int SignNumber=0)
    {
        PlayerPrefs.SetString("Level" + LevelNumber , "Open");

        if (SignNumber <= -1)
        {
            SignNumber = 0;
            if (!PlayerPrefs.HasKey("Sign" + SignNumber ))
            {
                if (PlayerPrefs.GetString("Sign" + SignNumber) != "Open")
                {
                    PlayerPrefs.SetString("Sign" + SignNumber, "Open");
                }
            }
            
            
        }else if (SignNumber >= 5)
        {
            SignNumber = 4;
            if (!PlayerPrefs.HasKey("Sign" + SignNumber))
            {
                if (PlayerPrefs.GetString("Sign" + SignNumber) != "Open")
                {
                    PlayerPrefs.SetString("Sign" + SignNumber, "Open");
                }
            }

            //PlayerPrefs.SetString("Sign" + SignNumber, "Open");
        }
        else
        {
            if (!PlayerPrefs.HasKey("Sign" + SignNumber))
            {
                if (PlayerPrefs.GetString("Sign" + SignNumber) != "Open")
                {
                    PlayerPrefs.SetString("Sign" + SignNumber, "Open");
                }
            }
            //PlayerPrefs.SetString("Sign" + SignNumber, "Open");
        }

    }




     
    //public void LoadLevel()
    //{
    //    int openLvlIndex = 0; 
    //    for (int i = 0; i < /*ParentLevel.transform.GetChild(0).childCount+1*/ levels.Length + 1; i++)
    //    {
    //        if (PlayerPrefs.HasKey("Level" + i))
    //        {
    //            if (PlayerPrefs.GetString("Level" + i) == "Open")
    //            {
    //                //print("opening");
    //                Buttons[i - 1].transform.tag = "Open";
    //                if(i == 10)
    //                {
    //                    levels[9].GetComponent<Animator>().enabled = false;
    //                }
    //                else
    //                {
    //                    levels[i].GetComponent<Animator>().enabled = false;
    //                }
    //                Buttons[i - 1].GetComponent<Image>().sprite = Unlock[i];
    //                openLvlIndex = i;
    //            }
    //        }
    //        else
    //        {
    //            if (i == 0) return;
    //            Buttons[i - 1].transform.tag = "Untagged";
    //            levels[i].GetComponent<Animator>().enabled = false;
    //            levels[i].GetComponent<Image>().sprite = lockSp;
    //        }
    //    }

    //    AnimateLastLvlNumber(ref openLvlIndex);
    //}

    //void AnimateLastLvlNumber(ref int index)
    //{
    //    //print("animate" + "index is " + index);
    //    int currentIndex = index - 1 ;
    //    if(index == 0)
    //    {
    //        currentIndex = 0;
    //    }
    //    if(index > 0)
    //    {
    //        levels[0].GetComponent<Animator>().enabled = false;
    //    }        
    //    levels[currentIndex].GetComponent<Animator>().enabled = true;
    //}

    public void LoadLevel()
    {
        int lastOpenIndex = 0;
        for(int i=1; i< levels.Length; i++)
        {
            int levelIndex = i + 1;
            if(PlayerPrefs.HasKey("Level" + levelIndex))
            {
                lastOpenIndex = i;
                DoOpenLevelThings(ref levelIndex);
            }
            else
            {
                DoCloseLevelThings(ref levelIndex);
            }            
        }

        AnimateLastLevelSprite(ref lastOpenIndex);
    }

    void DoOpenLevelThings(ref int i)
    {
        int index = i - 1;
        levels[index].transform.tag = "Open";
        levels[index].GetComponent<Image>().sprite = Unlock[index];        
    }

    void DoCloseLevelThings(ref int i)
    {
        int index = i - 1;
        levels[index].transform.tag = "Untagged";
        levels[index].GetComponent<Image>().sprite = lockSp;
    }

    void AnimateLastLevelSprite(ref int index)
    {
        for(int i=0; i<= 9; i++)
        {
            levels[i].GetComponent<Animator>().enabled = false;
        }
        levels[0].GetComponent<Image>().sprite = Unlock[0];
        levels[index].GetComponent<Animator>().enabled = true;
    }


}

