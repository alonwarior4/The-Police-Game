using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IMoneyAffectable
{
    #region Variables

    // Game manager Instance
    public static GameManager GM_Instance;
      
    float totalMoney = 0;
    public float TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            totalMoney = value;
            EventBroker.OnMoneyChange(totalMoney);
        }
    }

    [Header("place to show total money")]
    [SerializeField] Text whiteMoneyTextPlace;
    [SerializeField] Text blueMoneyTextPlace;

    [Header("Car Shared Config Values")]
    public float vehicleFadeTime;
    public float changeWaypointThreshold;
    public float rayLength;

    [Header("UI Global Configs")]
    public float timeScaleWhenDragSign;

    [Header("Vehicle stop pos waiting to respawn")]
    public Transform tappedCarGarage;

    float tappedCarsNumber;
    public float TappedCarsNumber
    {
        get => tappedCarsNumber;
        set
        {
            tappedCarsNumber = value;
            ChangeTappedCarsSlider();
            EventBroker.OnTappedCar(tappedCarsNumber);
        }
    }

    [Header("max slider values")]
    [SerializeField] float maxTapCars;
    [SerializeField] int maxAllowedCars;
    [Header("Slider Refrences")]
    [SerializeField] Slider tappedSlider;

    int allowedCars;
    public int AllowedCars
    {
        get => allowedCars;
        set
        {
            allowedCars = value;
            StartCoroutine(ChangeAllowedCarUI());
        }
    }

    [Header("parenOfPolicelights")]
    public Sprite OffLightPolice;
    [SerializeField] Image[] Lights;

    Vector3 lastOnLightTransform;
    //Sign[] sceneSigns;
    //[SerializeField] BotUiPanel botUiPanel;

    [HideInInspector] public Coroutine DragAndDrop;
    [SerializeField] WinLoseUI winLoseUi;

    #endregion



    private void Awake()
    {
        if(GM_Instance == null)
        {
            GM_Instance = this;
        }
    }

    private void OnEnable()
    {
        EventBroker.AddOnMoneyChangeObserver(this);
    }

    void Start()
    {
        winLoseUi.gameObject.SetActive(false);
        EventBroker.OnMoneyChange(totalMoney);
        AllowedCars = maxAllowedCars;
        //sceneSigns = FindObjectsOfType<Sign>();
    }

    public void OnMoneyChangeNotify(float totalMoney)
    {
        whiteMoneyTextPlace.text = totalMoney.ToString();
        blueMoneyTextPlace.text = totalMoney.ToString();
    }
   
    private void OnDisable()
    {
        EventBroker.RemoveMoneyChangeObserver(this);       
        GM_Instance = null;                                                        //free static memory usage
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TotalMoney += 30;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }

    private void ChangeTappedCarsSlider()
    {
        if (tappedCarsNumber > maxTapCars) return;

        float sliderValue = tappedCarsNumber / maxTapCars;
        tappedSlider.value = sliderValue;

        if (tappedCarsNumber == maxTapCars)
        {
            //TODO : win part , put everything when win here
            //print("you win");
            //gameplayUi.Win();
            winLoseUi.gameObject.SetActive(true);
            winLoseUi.Win();
        }        
    }

    IEnumerator ChangeAllowedCarUI()
    {  
        if(allowedCars > 0)
        {
            for (int i = maxAllowedCars - 1; i > allowedCars - 1; i--)
            {
                Lights[i].sprite = OffLightPolice;
            }

            yield return new WaitForEndOfFrame();
            lastOnLightTransform = Lights[allowedCars - 1].transform.position;
            Azhir.azhir_Instance.ChangeAzhirAnimObjPostion(lastOnLightTransform);
        }
        else
        {
            //TODO : lose part , put everything when lose here
            Lights[0].sprite = OffLightPolice;
            //print("you lose");
            //gameplayUi.Lose();
            winLoseUi.gameObject.SetActive(true);
            winLoseUi.Lose();
        }
    }

    //public void CheckToCloseInventory()
    //{
    //    botUiPanel.CloseInventoryPanel();
    //}
}
