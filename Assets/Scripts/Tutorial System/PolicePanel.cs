using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicePanel : MonoBehaviour
{
    [HideInInspector] public Sprite currentPoliceTxt;
    [SerializeField] Image textPlace;
    [SerializeField] Button okBtn;
    Animator policeAnimator;
    [SerializeField] GameObject rayBlocker;
    Canvas parentCanvas;
    string uiLayeruiSortingLayer = "UI";
    string tutorialSortingLayer = "Tutorial";

    int policeComeTriggerId => Animator.StringToHash("Come");
    int policeGoTriggerId => Animator.StringToHash("Go");
    int ChangeTxtTriggerId => Animator.StringToHash("ChangeText");

    GamePlayUiBtn uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<GamePlayUiBtn>();
    }


    private void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        parentCanvas.sortingLayerName = uiLayeruiSortingLayer;
        policeAnimator = GetComponent<Animator>();
    }    

    //use in animation event
    public void ChangeText()
    {
        textPlace.sprite = currentPoliceTxt;
    }

    //use in job manager
    public void InitializePoliceText(/*ref *//*PoliceTextSO startTxt*/ ref Sprite startTxt)
    {
        currentPoliceTxt = startTxt;
    }

    //use in job manager
    public void SetCurrentPliceText(/*ref PoliceTextSO newText*/ref Sprite newText)
    {
        currentPoliceTxt = newText;
    }

    //use in job manager
    public void PoliceCome()
    {
        if (uiManager.isPausePanelOpen)
            uiManager.ClosePausePanel();
        Time.timeScale = 0;
        parentCanvas.sortingLayerName = tutorialSortingLayer;
        policeAnimator.SetTrigger(policeComeTriggerId);
    }

    //use in job manager
    public void OkBtnPoliceGo()
    {
        //okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.RemoveListener(ChangeTxtOkFunction);
        okBtn.onClick.AddListener(PoliceGoAnimationOkFunction);        
    }

    //use in job manager
    public void OkBtnChangePoliceText()
    {
        //okBtn.onClick.RemoveAllListeners();
        okBtn.onClick.RemoveListener(PoliceGoAnimationOkFunction);
        okBtn.onClick.AddListener(ChangeTxtOkFunction);        
    }

    public float GetCurrentAnimationLength()
    {
        return policeAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

    //ON OK CLICK FUNCTION
    void PoliceGoAnimationOkFunction()
    {
        StartCoroutine(PoliceGoCoroutine());
    }

    IEnumerator PoliceGoCoroutine()
    {
        policeAnimator.SetTrigger(policeGoTriggerId);
        WaitForSecondsRealtime waitPoliceGo = new WaitForSecondsRealtime(1.75f);
        yield return waitPoliceGo;
        parentCanvas.sortingLayerName = uiLayeruiSortingLayer;
    }

    void ChangeTxtOkFunction()
    {
        policeAnimator.SetTrigger(ChangeTxtTriggerId);
    }

    public void ContinueTutoJobs()
    {
        if (Tutorial.t_Instance.currentJobManager.arrayIndex < Tutorial.t_Instance.currentJobManager.listCount - 1)
        {
            Tutorial.t_Instance.currentJobManager.arrayIndex++;
            Tutorial.t_Instance.ContinueTuto_Jobs();
        }
    }

    //public void SetOffRayBlocker()
    //{
    //    rayBlocker.SetActive(false);
    //}


    // SOUND PART
    public void PlayPoliceComeSound()
    {
        AudioManager.AM_Instance.PlayPoliceCome();        
    }

    public void PlayPoliceGoSound()
    {
        AudioManager.AM_Instance.PlayPoliceGo();
    }
}
