using LetterboxCamera;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SetScreenRatioState : MonoBehaviour
{
    [SerializeField] ForceCameraRatio forceCameraRatio;

    private void Awake()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float aspectRatio = screenWidth / screenHeight;
        if(aspectRatio < 1.77f)
        {
            forceCameraRatio.enabled = true;
        }
        else
        {
            forceCameraRatio.enabled = false;
        }
    }

}
