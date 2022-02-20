using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotUiPanel : MonoBehaviour
{
    //[SerializeField] Animator boardAnim;
    [SerializeField] GameObject bigImage;
    Animator inventoryAnim;
    //int openPanelBoolId => Animator.StringToHash("OpenPanel");
    int inventoryOffTriggerId => Animator.StringToHash("InventoryOff");
    WaitForEndOfFrame waitToEndOfFrame = new WaitForEndOfFrame();


    private void Start()
    {
        inventoryAnim = GetComponent<Animator>();
    }

    public void CloseInventoryPanel()
    {
        StartCoroutine(InventoryOff());
    }


    IEnumerator InventoryOff()
    {
        //if(boardAnim.GetParameter(0).defaultBool == true)
        //{
        //    boardAnim.SetBool(openPanelBoolId, false);
        //    yield return waitToEndOfFrame;
        //    WaitForSeconds waitPanelClose = new WaitForSeconds(boardAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        //    yield return waitPanelClose;
        //}
        bigImage.SetActive(false);
        //GetComponent<Animator>().SetTrigger(inventoryOffTriggerId);
        inventoryAnim.SetTrigger(inventoryOffTriggerId);
        yield return waitToEndOfFrame;
        WaitForSeconds waitInventoryGo = new WaitForSeconds(inventoryAnim.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        yield return waitInventoryGo;
        gameObject.SetActive(false);
    }
}
