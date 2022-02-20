using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tutorial : MonoBehaviour , ISavable
{
    // singleton instance
    public static Tutorial t_Instance;

    /*[HideInInspector] */public T_JobManager currentJobManager;
    [SerializeField] List<T_JobManager> tutorialQueue = new List<T_JobManager>();

    [Header("Tutorial controlable Cars")]
    [SerializeField] Vehicle[] sceneCars;

    public string pathOffset 
    {
        get
        {
            return "/TutorialData.bolbol";
        } 
    }

    #region Tutorial State bools
    //default
    public bool isFirstTimeDefaultTime = false;

    //totals
    public bool isFirstTimeCarCrime , isFirstTimeCarSideCrime , isFirstTimeSignMoney, isFirstTimeCameraMoney = false;

    //special cars
    public bool isFirstTimeNoTapCar, isFirstTimeMultiTapCar = false;

    //first time sign crime
    public bool isFirstTimeOneWaySign, isFirstTimeNoTurnRight, isFirstTimeNoTurning = false;
    #endregion

    #region Job Manager Refrence
    [SerializeField] T_JobManager defaultTutorial;

    [SerializeField] T_JobManager m_firstCarCrime;
    [SerializeField] T_JobManager m_firstCarSideCrime;
    [SerializeField] T_JobManager m_firstCameraMoney;
    [SerializeField] T_JobManager m_firstSignMoney;

    [SerializeField] T_JobManager m_firstTimeOneWaySign;
    [SerializeField] T_JobManager m_firstTimeNoTurnRightSign;
    [SerializeField] T_JobManager m_firstTimeNoTurningSign;

    [SerializeField] T_JobManager m_firstTimeNoTapCar;
    [SerializeField] T_JobManager m_firstTimeMultiTapCar;
    //[SerializeField] T_JobManager m_firstTimeInvisibleCar;    
    #endregion




    private void Awake()
    {
        if (t_Instance == null)
        {
            t_Instance = this;
        }
    }

    private void Start()
    {
        LoadTutorialData();
        if (defaultTutorial != null)
        {
            //tutorialQueue.Add(defaultTutorial);
            //currentJobManager = defaultTutorial;            
            //ContinueTuto_Jobs();

            CheckFirstTimeDefaultTutorial();
        }
        //ContinueTuto_Jobs();

        // TODO : Delete below lines and use save and load for save tutorial data , just uncomment for testing tutorials
        //isFirstTimeCarCrime = true;
        //isFirstTimeNoTurning = true;
        //isFirstTimeNoTurnRight = true;
        //isFirstTimeOneWaySign = true;
        //isFirstTimeSignMoney = true;
        //isFirstTimeCarSideCrime = true;
        //isFirstTimeCameraMoney = true;
    }

    public void SaveTutorialData()
    {
        SaveLoadSystem.Save(this, new TutorialData(this));
    }

    public void EndOfTutorial()
    {
        for(int i=0; i<sceneCars.Length; i++)
        {
            sceneCars[i].TutorialEnds();
        }
    }

    public void StartTutorial()
    {
        for(int i=0; i< sceneCars.Length; i++)
        {
            sceneCars[i].TutorialBegins();
        }
    }

    public void ContinueTuto_Jobs()
    {
        currentJobManager.ContinueTutorial();
    }

    public void JobListFinihsed()
    {
        tutorialQueue.Remove(currentJobManager);
        if(tutorialQueue.Count > 0)
        {
            currentJobManager = tutorialQueue[0];
            ContinueTuto_Jobs();
        }
        else
        {            
            currentJobManager = null;
        }
    }
    
    public void CheckContinueState(ContinueTutoActionState state)
    {
        //print("check continue state" + state.ToString());
        if (currentJobManager != null && currentJobManager.currentContinueState == state)
        {
           // print("continue state");
            ContinueTuto_Jobs();
        }
    }

    public void LoadTutorialData()
    {
        if(!PlayerPrefs.HasKey("FTTS"))    // first time tutotial save (ftts)
        {
            DeleteAllSaves();
            PlayerPrefs.SetInt("FTTS", 1);
        }
        else
        {
            TutorialData t_Data = SaveLoadSystem.Load<TutorialData>(this);
            isFirstTimeDefaultTime = t_Data.isFirstTimeDefault;
            isFirstTimeCarCrime = t_Data.isFirstTimeCarCrime;
            isFirstTimeCarSideCrime = t_Data.isFirstTimeCarSideCrime;
            isFirstTimeSignMoney = t_Data.isFirstTimeSignMoney;
            isFirstTimeCameraMoney = t_Data.isFirstTimeCameraMoney;

            isFirstTimeNoTapCar = t_Data.isFirstTimeNoTapCar;
            isFirstTimeMultiTapCar = t_Data.isFirstTimeMultiTapCar;

            isFirstTimeOneWaySign = t_Data.isFirstTimeOneWaySign;            
            isFirstTimeNoTurning = t_Data.isFirstTimeNoTurning;          
            isFirstTimeNoTurnRight = t_Data.isFirstTimeNoTurnRight;
        }        
    }

    public void DeleteAllSaves()
    {
        isFirstTimeDefaultTime = false;
        isFirstTimeCarCrime = false;
        isFirstTimeCarSideCrime = false;
        isFirstTimeSignMoney = false;
        isFirstTimeCameraMoney = false;

        isFirstTimeNoTapCar = false;
        isFirstTimeMultiTapCar = false;

        isFirstTimeOneWaySign = false;
        isFirstTimeNoTurning = false;
        isFirstTimeNoTurnRight = false;

        SaveLoadSystem.Save(this, new TutorialData(this));
    }

    #region Tutorial States Functions 

    public void CheckFirstTimeDefaultTutorial()
    {
        if (isFirstTimeDefaultTime) return;
        //isFirstTimeDefaultTime = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(defaultTutorial);
        if(currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            ContinueTuto_Jobs();
        }
    }

    public void CheckFirstTimeCarCrime(Transform carTransform)
    {
        if (isFirstTimeCarCrime) return;
        //isFirstTimeCarCrime = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstCarCrime);
        if(currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //print("current job is null and array index is " + currentJobManager.arrayIndex);
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].carPos = carPos;
            currentJobManager.currentTutoCarTransform = carTransform;
            ContinueTuto_Jobs();
        }
    }

    public void CheckFirstTimeSideCrime(Transform carTransform)
    {
        if (isFirstTimeCarSideCrime) return;
        //isFirstTimeCarSideCrime = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstCarSideCrime);
        if(currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].carPos = carPos;
            currentJobManager.currentTutoCarTransform = carTransform;
            ContinueTuto_Jobs();
        }
    }

    public void CheckFirstTimeSignMoney(Vector2 signPos)
    {
        if (isFirstTimeSignMoney) return;
        //isFirstTimeSignMoney = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstSignMoney);
        if(currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].firstPos = signPos;
            currentJobManager.currentItemPos = signPos;
            ContinueTuto_Jobs();
        }
    }

    public void CheckFirstTimeCameraMoney(Vector2 camPos)
    {
        if (isFirstTimeCameraMoney) return;
        //isFirstTimeCameraMoney = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstCameraMoney);
        if (currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].firstPos = camPos;
            currentJobManager.currentItemPos = camPos;
            ContinueTuto_Jobs();
        }
    }

    public void CheckFirstTimeOneWaySignCrime(Transform carPos)
    {
        if (isFirstTimeOneWaySign) return;
        //isFirstTimeOneWaySign = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstTimeOneWaySign);
        if (currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].firstPos = signPos;
            currentJobManager.currentTutoCarTransform = carPos;
            ContinueTuto_Jobs();
        }
    }

    //public void CheckFirstTimeNoTurnLeftSignCrime(Transform carPos)
    //{
    //    if (isFirstTimeNoTurnLeft) return;
    //    isFirstTimeNoTurnLeft = true;
    //    SaveLoadSystem.Save(this, new TutorialData(this));

    //    tutorialQueue.Add(m_firstTimeNoTurnLeftSign);
    //    if (currentJobManager == null)
    //    {
    //        currentJobManager = tutorialQueue[0];
    //        //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].firstPos = signPos;
    //        currentJobManager.currentTutoCarTransform = carPos;
    //        ContinueTuto_Jobs();
    //    }
    //}

    public void CheckFirstTimeNoTurnRightSignCrime(Transform carPos)
    {
        if (isFirstTimeNoTurnRight) return;
        //isFirstTimeNoTurnRight = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstTimeNoTurnRightSign);
        if (currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].firstPos = signPos;
            currentJobManager.currentTutoCarTransform = carPos;
            ContinueTuto_Jobs();
        }
    }

    public void CheckFirstTimeNoTurningSignCrime(Transform carPos)
    {
        if (isFirstTimeNoTurning) return;
        //isFirstTimeNoTurning = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstTimeNoTurningSign);
        if (currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].firstPos = signPos;
            currentJobManager.currentTutoCarTransform = carPos;
            ContinueTuto_Jobs();
        }
    }

    //public void CheckFirstTimeNoParkingSignCrime(Transform carPos)
    //{
    //    if (isFirstTimeNoParking) return;
    //    isFirstTimeNoParking = true;
    //    SaveLoadSystem.Save(this, new TutorialData(this));

    //    tutorialQueue.Add(m_firstTimeNoParkingSign);
    //    if (currentJobManager == null)
    //    {
    //        currentJobManager = tutorialQueue[0];
    //        //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].firstPos = signPos;
    //        currentJobManager.currentTutoCarTransform = carPos;
    //        ContinueTuto_Jobs();
    //    }
    //}

    public void CheckFirstTimeNoTapCar(Transform carTransform)
    {
        if (isFirstTimeNoTapCar) return;
        //isFirstTimeNoTapCar = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstTimeNoTapCar);
        if (currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].carPos = carPos;
            currentJobManager.currentTutoCarTransform = carTransform;
            ContinueTuto_Jobs();
        }
    }

    public void CheckFirstTimeMultiTapCar(Transform carTransform)
    {
        if (isFirstTimeMultiTapCar) return;
        //isFirstTimeMultiTapCar = true;
        //SaveLoadSystem.Save(this, new TutorialData(this));

        tutorialQueue.Add(m_firstTimeMultiTapCar);
        if (currentJobManager == null)
        {
            currentJobManager = tutorialQueue[0];
            //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].carPos = carPos;
            currentJobManager.currentTutoCarTransform = carTransform;
            ContinueTuto_Jobs();
        }
    }

    //public void CheckFirstTimeInvisbleCar(Transform carTransform)
    //{
    //    if (isFirstTimeInvisCar) return;
    //    isFirstTimeInvisCar = true;
    //    SaveLoadSystem.Save(this, new TutorialData(this));

    //    tutorialQueue.Add(m_firstTimeInvisibleCar);
    //    if (currentJobManager == null)
    //    {
    //        currentJobManager = tutorialQueue[0];
    //        //currentJobManager.t_jobWorks[currentJobManager.arrayIndex].carPos = carPos;
    //        currentJobManager.currentTutoCarTransform = carTransform;
    //        ContinueTuto_Jobs();
    //    }
    //}

    #endregion

    private void OnDisable()
    {
        t_Instance = null;
    }
}