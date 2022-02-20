using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// For horizontal needs horizontal scrollbar and for vertical needs vertical scrollbar
// Better use with horizontal and vertical layoutgroup 
// Be sure to fix layout group padding and spacing setting for better resault
// Can use viewPort for masking and also it will be 0 value place for scrollbar
// Instruction : a panel with scrollrect component ===> a panel with image and mask as view port ===> a panel with layoutGroup and this(ScrollSnap) component as scrollrect content ===>
   // all UI Elements that wanted to has this snap effect
   // *** all instruction levels are child of previuos one ***


public class ScrollSnap : MonoBehaviour
{

    [Header("Snap Setting")]
    [Range(0.1f , 1f)] [SerializeField] float snapforce;

    [Header("In Middle Wiewport Config")]
    [SerializeField] float selectSize;
    [Range(0f, 1f)] [SerializeField] float selectSizeAnimSpeed; 

    [Header("Default Config")]
    [SerializeField] float defaultSize;
    [Range(0f, 1f)] [SerializeField] float defaultSizeAnimSpeed;

    [Header("Controlable Scrollbar")]
    [SerializeField] Scrollbar scrollbar;
    float scrollPos;
    float[] ChildPos;
    float distance;


    private void Start()
    {
        ChildPos = new float[transform.childCount];

        distance = 1f / (ChildPos.Length - 1f);
        for (int i = 0; i < ChildPos.Length; i++)
        {
            ChildPos[i] = distance * i;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.value;
        }
        else
        {
            for (int i = 0; i < ChildPos.Length; i++)
            {
                if (scrollPos < ChildPos[i] + (distance /2) && scrollPos > ChildPos[i] - (distance /2))
                {
                    scrollbar.value = Mathf.Lerp(scrollbar.value, ChildPos[i], snapforce);
                }
            }
        }


        for (int i = 0; i < ChildPos.Length; i++)
        {
            if (scrollPos < ChildPos[i] + (distance / 2) && scrollPos > ChildPos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, Vector2.one * selectSize, selectSizeAnimSpeed);
                for (int j = 0; j < ChildPos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale,
                            Vector2.one * defaultSize, defaultSizeAnimSpeed);
                    }
                }
            }
        }
    }

}
