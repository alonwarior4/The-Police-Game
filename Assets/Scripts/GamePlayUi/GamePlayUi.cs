using UnityEngine;
using UnityEngine.UI;

public class GamePlayUi : MonoBehaviour
{
    public Image FillAmountTop;
    public Image FillAmountDown;

    GameObject PoliceSliderTop;
    GameObject PoliceSliderDown;

    public GameObject CamTop;
    public GameObject CamDown;

    public GameObject PopUp;



    void Awake()
    {
        /*FillAmountTop = FillAmountTop.GetComponent<Image>();
        FillAmountDown = FillAmountDown.GetComponent<Image>();

        FillAmountTop.fillAmount = 0;
        FillAmountDown.fillAmount = 0;


        checkMoney();

        PoliceSliderTop = FillAmountTop.transform.parent.parent.gameObject;
        PoliceSliderDown = FillAmountDown.transform.parent.parent.gameObject;*/  
    }



   /* private void Start()
    {
        UserMoneyInGame(5);
    }*/


    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.U))
        {
            increaseTotalMoney();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TabTopCamForGetIt();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (PopUp.transform.localScale.x == 0)
            {
                PopUp.GetComponent<Animator>().SetBool("popUpChange", false);
            }
            else
            {
                PopUp.GetComponent<Animator>().SetBool("popUpChange", true);
                PopUp.transform.GetChild(0).GetComponent<Animation>().Play("idleIcon");
            }
        }*/
    }    

    public void TabTopCamForGetIt()
    {
        if (FillAmountTop.fillAmount == 1 && GameManager.GM_Instance.TotalMoney >= 10)
        {
            PoliceSliderTop.SetActive(false);
            CamTop.GetComponent<SpriteRenderer>().color = new Color32(173, 173, 173, 255);
            GameManager.GM_Instance.TotalMoney -= 10;
            checkMoney();
        }
    }    
    
    public void TabDownCamForGetIt()
    {
        if (FillAmountDown.fillAmount==1 && GameManager.GM_Instance.TotalMoney>=10 )
        {
            PoliceSliderDown.SetActive(false);
            CamDown.GetComponent<SpriteRenderer>().color = new Color32(173, 173, 173, 255);
            GameManager.GM_Instance.TotalMoney -= 10;
            checkMoney();
        }      
    }

    public void checkMoney()
    {
        if(GameManager.GM_Instance.TotalMoney >= 10)
        {
            FillAmountTop.fillAmount = 1;
            FillAmountDown.fillAmount = 1;
        }
        else
        {
            FillAmountTop.fillAmount = GameManager.GM_Instance.TotalMoney * 0.1f;
            FillAmountDown.fillAmount = GameManager.GM_Instance.TotalMoney * 0.1f;
        }
    }

    public void increaseTotalMoney()
    {
        GameManager.GM_Instance.TotalMoney += 1f;
        checkMoney();
    }

    //-------------------------------------------------------------User Money Ui---------------------------------------------
    
    [Header("UserMoney")]
    public Text BlueMoney;
    public Text WhiteMoney;

    [Header("BoardPrices")]
    public GameObject[] PricesBoard;

    public void UserMoneyInGame(int userMoney)
    {
        BlueMoney.text = userMoney.ToString();
        WhiteMoney.text = userMoney.ToString();
    }

}
