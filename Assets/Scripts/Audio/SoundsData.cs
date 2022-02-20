using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Sound Data" , fileName = "Sound Data 1")]
public class SoundsData : ScriptableObject
{
    [Header("Gameplay Sounds")]
    public Sound NC_PopUp;    
    public Sound NC_PopUpTap;
    public Sound MainCrimeTapOnCar;
    public Sound MainCrimeNoTap;
    public Sound spec_Enter;
    public Sound Tap_NTC;
    public Sound MultiTap_Fire;
    public Sound SignLaserSound;
    public Sound PoliceCome;
    public Sound PoliceGo;

    [Header("UI Sounds")]
    public Sound UiClick;
    public Sound ItemPick;
    public Sound ItemDrop;
    public Sound ItemOpened;
    public Sound AzhirSound;
    public Sound ArrowUp;
    public Sound ScrollSign;
    public Sound WinSound;
    public Sound ItemWrongPalcement;
}


[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [Range(0 , 1f)] public float volume;
}



