using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiTapCar : SpecialCar
{
    int tapCount;
    int tapTriggerId => Animator.StringToHash("Tap");
    bool isWaitingToPlayFireAnimation;    



    protected override void Start()
    {
        base.Start();
        isWaitingToPlayFireAnimation = false;
    }

    public override void CheckSpecTutorial()
    {
        Tutorial.t_Instance.CheckFirstTimeMultiTapCar(transform);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!isCanTouch || tapCount > 2 || isWaitingToPlayFireAnimation) return;

        tapCount++;
        if(tapCount == 1)
        {
            //TODO : complete fire animation
            movingObjectAnim.SetTrigger(tapTriggerId);
            AudioManager.AM_Instance.PlayMultiTapFire();
            StartCoroutine(WaitToFireEnds());
        }
        else if(tapCount == 2)
        {
            TouchWork?.Invoke();
        }
        //CheckForTouchwork();
    }

    public override void DoSomeThingBeforeResapwn()
    {
        tapCount = 0;
        isCanTouch = false;
    }

    IEnumerator WaitToFireEnds()
    {
        isWaitingToPlayFireAnimation = true;
        yield return new WaitForEndOfFrame();
        WaitForSeconds waitFireAnim = new WaitForSeconds(0.3f);
        yield return waitFireAnim;
        isWaitingToPlayFireAnimation = false;
    }

}
