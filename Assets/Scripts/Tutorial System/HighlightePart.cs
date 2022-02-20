using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightePart : MonoBehaviour
{
    [SerializeField] Transform holeTransform;
    Vector3 currentHolePosition;
    Animator h_Animator;

    //aniamtor parameters
    int fadeInTriggerId => Animator.StringToHash("FadeIn");
    int fadeOutTriggerId => Animator.StringToHash("FadeOut");



    private void Start()
    {
        h_Animator = GetComponent<Animator>();
    }

    //use in tutorial chain
    public void SetCurrentHolePosition(Vector2 newPos)
    {
        currentHolePosition = newPos;
    }

    //use in animation event
    public void SetHolePosition()
    {
        holeTransform.position = currentHolePosition;
    }

    //use in job manager
    public void FadeIn()
    {
        h_Animator.SetTrigger(fadeInTriggerId);
    }

    //use in job manager
    public void FadeOut()
    {
        h_Animator.SetTrigger(fadeOutTriggerId);
    }

    public float GetCurrentAnimationLength()
    {
        return h_Animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
