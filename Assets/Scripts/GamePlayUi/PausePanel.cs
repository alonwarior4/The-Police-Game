using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    Canvas parentCanvas;

    private void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void SetLayerUIOnTop()
    {
        parentCanvas.sortingOrder = 200; 
    }

    public void SetDefaultLayerUI()
    {
        parentCanvas.sortingOrder = 100;
    }
}
