using UnityEngine;
using UnityEngine.UI;

public class SwipBoard : MonoBehaviour
{
    public GameObject scrollBar;
    private float scrollPos = 0;
    private float[] pos;
    bool isCanPlaySound;


    public float distance;

    private void Start()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }


    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i]+(distance/2) && scrollPos > pos[i] -(distance/2))
                {
                    scrollBar.GetComponent<Scrollbar>().value =
                        Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2) )
            {

                transform.GetChild(i).localScale = Vector2.Lerp(transform.
                    GetChild(i).localScale,new Vector2(1.5f,1.5f),0.1f );
                //if (isCanPlaySound)
                //{
                //    AudioManager.AM_Instance.PlayScrollSign();
                //    isCanPlaySound = false;
                //}

                transform.GetChild(i).localPosition = Vector2.Lerp(transform.GetChild(i).
                    localPosition, new Vector2(transform.GetChild(i).localPosition.x,40f),0.1f);


                //OpenTutorialPanelInInventory
                openPanelTutorial(i,transform);
                

                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        //change Scale Item
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale,
                            new Vector2(0.8f, 0.8f), 0.1f);

                        //change Position Item
                        transform.GetChild(j).localPosition = new Vector2(transform.GetChild(j).
                            localPosition.x, 0);


                        //CloseTutorialPanelInInventory
                        closePanelTutorial(j);
                    }
                }
            }
            //else
            //{
            //    isCanPlaySound = true;
            //}
        }
    }

    [Header("PanelManagerInventory")]
    public GameObject parentPanel;
    public GameObject parentSigns;
    public GameObject InvenBtnMainMenu;
    public void openPanelTutorial(int indexPanel,Transform transform)
    {
        parentPanel.transform.GetChild(indexPanel).gameObject.SetActive(true);

        if (transform.GetChild(indexPanel).GetChild(1).gameObject.activeSelf)
        {
            InvenBtnMainMenu.transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(indexPanel).GetChild(1).gameObject.SetActive(false);
            PlayerPrefs.SetString("Sign" + indexPanel, "Displayed");            
        }
        if (!transform.GetChild(indexPanel).GetChild(0).gameObject.activeSelf) 
        {
            parentPanel.transform.GetChild(indexPanel).GetChild(0).gameObject.SetActive(true);
            parentPanel.transform.GetChild(indexPanel).GetChild(1).gameObject.SetActive(false);
        }
    }
    
    public void closePanelTutorial(int indexPanel)
    {
        parentPanel.transform.GetChild(indexPanel).gameObject.SetActive(false);
       //parentPanel.transform.GetChild(indexPanel).GetComponent<Animator>().SetBool("OpenPanel",false);
    }
    
    
}
