using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{



    
    private void Start()
    {
        LoadSignStatus();
    }

    private void OnEnable()
    {
        LoadSignStatus();
    }


    //-----------------------------------------------Notifiction-----------------------------------
    [SerializeField]
    [Header("notifictionField")]
    public GameObject NotifictionInInventory;
    //GameObject Notifiction;

    [Header("SignSprite")]
    public List<Sprite> Signs;

    [Header("InventoryBtnMainMenu")]
    public GameObject MainInvenBtn;

    [SerializeField] Sprite BaseCircleSign;

    
   

   
    //--------------------------------------------------SelectItem--------------------------------------


    

    


    public void LoadSignStatus()
    {

        for (int i = 1; i < 4; i++)
        {
           
            if (PlayerPrefs.HasKey("Sign" + i))
            {
                if (PlayerPrefs.GetString("Sign" + i) == "Open")
                {
                    if (NotifictionInInventory.transform.GetChild(i).GetChild(0).transform != null)
                    {

                        if (NotifictionInInventory.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
                        {
                            //NotifictionInInventory.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                            NotifictionInInventory.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                            NotifictionInInventory.transform.GetChild(i)
                                .GetComponent<Image>().sprite = Signs[i];
                            //print(NotifictionInInventory.transform.GetChild(i - 1).GetChild(1).gameObject.name + i);
                            NotifictionInInventory.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                            MainInvenBtn.transform.GetChild(0).gameObject.SetActive(true);
                        }

                    }
                }
            }
            else
            {
                //print("no sign");
                //if (NotifictionInInventory.transform.GetChild(i).GetChild(1).transform != null)
                //{
                //    if (NotifictionInInventory.transform.GetChild(i).GetChild(1).gameObject.activeSelf)
                //    {
                //        NotifictionInInventory.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                //        NotifictionInInventory.transform.GetChild(i)
                //            .GetComponent<Image>().sprite = BaseCircleSign;
                //        NotifictionInInventory.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                //        MainInvenBtn.transform.GetChild(0).gameObject.SetActive(false);
                //    }
                //}
                if (i == 0) return;
                NotifictionInInventory.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                NotifictionInInventory.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

                NotifictionInInventory.transform.GetChild(i).GetComponent<Image>().sprite = BaseCircleSign;
                //MainInvenBtn.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        

        for (int i = 0; i < NotifictionInInventory.transform.childCount; i++)
        {
            if (PlayerPrefs.HasKey("Sign" + i))
            {
                if (PlayerPrefs.GetString("Sign" + i) == "Displayed")
                {

                    if (NotifictionInInventory.transform.GetChild(i).GetChild(1) != null)
                    {
                        NotifictionInInventory.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                        NotifictionInInventory.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                        NotifictionInInventory.transform.GetChild(i).GetComponent<Image>().sprite=Signs[i];
                        
                       // MainInvenBtn.transform.GetChild(0).gameObject.SetActive(false);
                    } 
                }
            }
        }

    }

    public void SetNotifOff()
    {
        MainInvenBtn.transform.GetChild(0).gameObject.SetActive(false);
    }



}
