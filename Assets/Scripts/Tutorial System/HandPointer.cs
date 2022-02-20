using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


public class HandPointer : MonoBehaviour 
{
    Vector2 loopAnimPos;
    Vector2 firstPos;
    Vector2 lastPos;    
    float duration;
    float frameRate = 60;
    Coroutine DragAndDropCoroutine;                  // Drag and Drop Corutine
    Animator pointerAnimator;
    string triggerName;

    //hand Transform
    Transform pointerTransform;

    //animator parameters
    int tapHoldTriggerId => Animator.StringToHash("TapHold");
    int tapReleaseTriggerId => Animator.StringToHash("TapRelease");
    int tapAnimTriggerId => Animator.StringToHash("TapAnim");
    int handOffTriggerId => Animator.StringToHash("HandOff");

    //wait cash
    WaitForEndOfFrame waitToEndFrame = new WaitForEndOfFrame();





    private void Start()
    {
        pointerAnimator = GetComponent<Animator>();
        pointerTransform = transform.GetChild(0);
    }

    //use in tutorial instance maybe
    public void SetDragAndDropValues(ref Vector2 firstPos , ref Vector2 lastPos , ref float duration)
    {
        this.firstPos = firstPos;
        this.lastPos = lastPos;
        this.duration = duration;
    }

    public void SetloopAnimPosition(Vector2 loopAnimPos)
    {
        this.loopAnimPos = loopAnimPos;
    }

    //use in job manager

    public void PlayDragAndDropAnimation()
    {
        DragAndDropCoroutine =  StartCoroutine(HandPointerCoroutine());        
    }

    public void SetTriggerName(ref string name)
    {
        triggerName = name;
    }

    //use in job manager
    public void StopDragAndDropAnimation()
    {
        if (DragAndDropCoroutine != null)
        {
            StopCoroutine(DragAndDropCoroutine);            
        }
    }

    //use in job manager
    public void PlayPointerLoopAnim()
    {
        StopDragAndDropAnimation();
        pointerTransform.position = loopAnimPos;
        pointerAnimator.SetTrigger(tapAnimTriggerId);
    }

    //use in job manager
    public void HandOff()
    {
        pointerAnimator.SetTrigger(handOffTriggerId);
    }

    IEnumerator HandPointerCoroutine()
    {        
        while (true)
        {
            //print("Draging and Droping");
            pointerTransform.position = firstPos;

            pointerAnimator.SetTrigger(tapHoldTriggerId);
            yield return waitToEndFrame;
            WaitForSecondsRealtime waitToTapHoldAnim = new WaitForSecondsRealtime(pointerAnimator.GetCurrentAnimatorStateInfo(0).length);
            yield return waitToTapHoldAnim;

            for (float f=0 ; f< duration + 0.1f ; f+= Time.unscaledDeltaTime)
            {
                pointerTransform.position = Vector2.Lerp(firstPos, lastPos, Mathf.Min(1, f / duration));
                yield return waitToEndFrame;
            }

            pointerAnimator.SetTrigger(tapReleaseTriggerId);
            yield return waitToEndFrame;
            WaitForSecondsRealtime waitToTapReleaseAnim = new WaitForSecondsRealtime(pointerAnimator.GetCurrentAnimatorStateInfo(0).length);
            yield return waitToTapReleaseAnim;
        }
    }

    public float GetCurrentAnimationLength()
    {
        return pointerAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

    
}
