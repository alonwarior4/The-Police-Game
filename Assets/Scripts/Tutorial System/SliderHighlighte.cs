using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHighlighte : MonoBehaviour
{
    [SerializeField] Transform squareMask;
    Animator sliderHighlighteAnim;
    int fadeInTriggerId => Animator.StringToHash("FadeIn");
    int fadeOutTriggerId => Animator.StringToHash("FadeOut");

    Vector2 squarePos;
    [HideInInspector] public Vector2 sceneDefaultPos;

    private void Awake()
    {
        sceneDefaultPos = new Vector2(-0.05f , 4.46f);
    }

    void Start()
    {
        sliderHighlighteAnim = GetComponent<Animator>();
    }

    public void SetSquarePos(ref Vector2 newPos)
    {
        squarePos = newPos;
    }

    //use in job manager
    public void SliderHighlighteFadeIn()
    {
        sliderHighlighteAnim.SetTrigger(fadeInTriggerId);
    }

    //use in job manager
    public void SliderHighlighteFadeOut()
    {
        sliderHighlighteAnim.SetTrigger(fadeOutTriggerId);
    }

    public void SetSquareMaskPositionInAnimation()
    {
        squareMask.position = squarePos;
    }


    public float GetCurrentAnimationDuration()
    {
        return sliderHighlighteAnim.GetCurrentAnimatorStateInfo(0).length;
    }
    
    
}
