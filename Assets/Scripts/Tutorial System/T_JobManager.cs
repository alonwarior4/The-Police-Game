using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


public enum ContinueTutoActionState
{
    None,
    TapMainCrimeCar, TapSideCrimeCar,
    placeCamera,
    TapOkButton,
    PlaceOneWaySign, PlaceNoTurnLeftSign, PlaceNoTurnRightSign, PlaceNoTurningAllowedSign, placeNoParkingSign,
    TapInvisCar, TapMultiTapCar
}

public enum TutorialType
{
    HiAleyk , FirstCrimeCar , FirstBuySign , UTurn , NoEnter , NoTurnRight , firstBuyCamera , FirstSideCrime , MultiTap , NoTapCar
}

public class T_JobManager : MonoBehaviour
{
    [SerializeField] TutorialType tutorialType;
    public List<T_Job> t_jobWorks;
    [SerializeField] PolicePanel policePanel;
    [SerializeField] HighlightePart circleHighlighte;
    [SerializeField] HandPointer handPointer;
    [SerializeField] SliderHighlighte sliderHighlighte;
    [SerializeField] GameObject rayBlocker;
    WaitForEndOfFrame waitToEndFrame = new WaitForEndOfFrame();

    [HideInInspector] public Transform currentTutoCarTransform;
    [HideInInspector] public Vector2 currentItemPos;

    [HideInInspector] public int listCount;
    public int arrayIndex = 0;

    [HideInInspector] public ContinueTutoActionState currentContinueState = ContinueTutoActionState.None;


    private void Awake()
    {
        listCount = t_jobWorks.Count;
    }

    IEnumerator TrainingChain()
    {
        for(int i=arrayIndex ; i< listCount; i++)
        {
            //handPointer.StopDragAndDropAnimation();
            if (t_jobWorks[i].delay > 0)
            {
                //print("delaying");
                WaitForSecondsRealtime delayWait = new WaitForSecondsRealtime(t_jobWorks[i].delay);
                yield return delayWait;
            }

            switch (t_jobWorks[i].jobWork)
            {

                case JobWorks.PoliceCome:
                    //print("police come");
                    policePanel.InitializePoliceText(ref t_jobWorks[i].PoliceTxt);
                    policePanel.PoliceCome();
                    if (t_jobWorks[i].isSimultaneous == false)
                    {
                        yield return waitToEndFrame;
                        WaitForSecondsRealtime waitComeAnim = new WaitForSecondsRealtime(policePanel.GetCurrentAnimationLength());
                        yield return waitComeAnim;
                    }
                    break;

                case JobWorks.OK_PoliceGo:
                    //print("police go");
                    policePanel.OkBtnPoliceGo();
                    t_jobWorks[i].continueState = ContinueTutoActionState.TapOkButton;
                    yield break;

                case JobWorks.OK_PoliceChangeTxt:
                    //print("chtxt");
                    policePanel.SetCurrentPliceText(ref t_jobWorks[i].PoliceTxt);
                    policePanel.OkBtnChangePoliceText();
                    t_jobWorks[i].continueState = ContinueTutoActionState.TapOkButton;
                    yield break;

                case JobWorks.CH_FadeIn:
                    //print("circle highlighte fade in");
                    Vector2 fadeInPos = (t_jobWorks[i].def_Pos == Vector2.zero) ? (Vector2)currentTutoCarTransform.position : t_jobWorks[i].def_Pos;
                    circleHighlighte.SetCurrentHolePosition(/*t_jobWorks[i].carPos*//*currentTutoCarTransform.position*/fadeInPos);
                    circleHighlighte.FadeIn();
                    if (t_jobWorks[i].isSimultaneous == false)
                    {
                        yield return waitToEndFrame;
                        //print("wai time is " + circleHighlighte.GetCurrentAnimationLength());
                        WaitForSecondsRealtime waitFadeIn = new WaitForSecondsRealtime(circleHighlighte.GetCurrentAnimationLength());
                        yield return waitFadeIn;
                    }
                    break;

                case JobWorks.CH_FadeOut:
                    //print("circle highlighte fade out");
                    circleHighlighte.FadeOut();
                    if (t_jobWorks[i].isSimultaneous == false)
                    {
                        yield return waitToEndFrame;
                        WaitForSecondsRealtime waitFadeOut = new WaitForSecondsRealtime(circleHighlighte.GetCurrentAnimationLength());
                        yield return waitFadeOut;
                    }
                    break;

                case JobWorks.PointerAnim:
                    //print("pointer anim");
                    if (t_jobWorks[i].def_Pos == Vector2.zero)
                    {
                        //print("car pos hand pos is " + currentTutoCarTransform);
                        handPointer.SetloopAnimPosition(/*ref t_jobWorks[i].carPos*/currentTutoCarTransform.position);
                    }
                    else
                    {
                        //print("default hand pos");
                        handPointer.SetloopAnimPosition(t_jobWorks[i].def_Pos);
                    }

                    handPointer.PlayPointerLoopAnim();
                    break;

                case JobWorks.Drag_Drop:
                    //print("drag and drop");
                    Vector2 firstPos;
                    Vector2 lastPos;
                    firstPos = (t_jobWorks[i].firstPosTransform == null) ? currentItemPos : (Vector2)t_jobWorks[i].firstPosTransform.position;
                    lastPos = (t_jobWorks[i].lastPosTransform == null) ? t_jobWorks[i].lastPos : (Vector2)t_jobWorks[i].lastPosTransform.position;
                    handPointer.SetDragAndDropValues(ref firstPos, ref lastPos, ref t_jobWorks[i].Duration);
                    handPointer.PlayDragAndDropAnimation();
                    break;

                case JobWorks.StopHandPointer:
                    //print("stop hand pointer");
                    handPointer.StopDragAndDropAnimation();
                    handPointer.HandOff();
                    break;
                    
                case JobWorks.TimeScale:
                    //print("time scale");
                    Time.timeScale = t_jobWorks[i].timeScale;
                    break;

                case JobWorks.BreakState:
                    //print("brake state");
                    currentContinueState = t_jobWorks[i].continueState;
                    arrayIndex++;
                    yield break;

                case JobWorks.SH_FadeIn:
                    //print("Square highlighte");
                    Vector2 squarePos = (t_jobWorks[i].def_Pos == Vector2.zero) ? sliderHighlighte.sceneDefaultPos : t_jobWorks[i].def_Pos;
                    sliderHighlighte.SetSquarePos(ref squarePos);
                    sliderHighlighte.SliderHighlighteFadeIn();
                    if (t_jobWorks[i].isSimultaneous == false)
                    {
                        yield return waitToEndFrame;
                        WaitForSecondsRealtime waitFadeIn = new WaitForSecondsRealtime(sliderHighlighte.GetCurrentAnimationDuration());
                        yield return waitFadeIn;
                    }
                    break;

                case JobWorks.SH_FadeOut:
                    //print("Sh Fade out");
                    sliderHighlighte.SliderHighlighteFadeOut();
                    if (t_jobWorks[i].isSimultaneous == false)
                    {
                        yield return waitToEndFrame;
                        WaitForSecondsRealtime waitFadeOut = new WaitForSecondsRealtime(sliderHighlighte.GetCurrentAnimationDuration());
                        yield return waitFadeOut;
                    }
                    break;

                case JobWorks.BlockRaycast:
                    //print("ray blocked");
                    rayBlocker.SetActive(true);
                    break;

                case JobWorks.UnBlockRaycast:
                    //print("ray unblocked");
                    rayBlocker.SetActive(false);
                    break;

                default:
                    break;
            }           

            if (arrayIndex < listCount - 1)
            {
                arrayIndex++;
            }
            else
            {
                //print("tuto is finished");
                SaveTutorialState();
                Tutorial.t_Instance.JobListFinihsed();
            }
        }
    }

    public void ContinueTutorial()
    {
        StartCoroutine(TrainingChain());
    }  
    
    void SaveTutorialState()
    {
        switch (tutorialType)
        {
            case TutorialType.HiAleyk:
                Tutorial.t_Instance.isFirstTimeDefaultTime = true;                                
                break;
            case TutorialType.FirstCrimeCar:
                Tutorial.t_Instance.isFirstTimeCarCrime = true;
                break;
            case TutorialType.FirstBuySign:
                Tutorial.t_Instance.isFirstTimeSignMoney = true;
                break;
            case TutorialType.UTurn:
                Tutorial.t_Instance.isFirstTimeNoTurning = true;
                break;
            case TutorialType.NoEnter:
                Tutorial.t_Instance.isFirstTimeOneWaySign = true;
                break;
            case TutorialType.NoTurnRight:
                Tutorial.t_Instance.isFirstTimeNoTurnRight = true;
                break;
            case TutorialType.firstBuyCamera:
                Tutorial.t_Instance.isFirstTimeCameraMoney = true;
                break;
            case TutorialType.FirstSideCrime:
                Tutorial.t_Instance.isFirstTimeCarSideCrime = true;
                break;
            case TutorialType.MultiTap:
                Tutorial.t_Instance.isFirstTimeMultiTapCar = true;
                break;
            case TutorialType.NoTapCar:
                Tutorial.t_Instance.isFirstTimeNoTapCar = true;
                break;
            default:
                break;
        }

        Tutorial.t_Instance.SaveTutorialData();
    }
}
