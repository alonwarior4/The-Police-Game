using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BolBolUtility.BolBolInGameTools;


public enum ClipType
{
    UI , Music , Gameplay , Ambient
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager AM_Instance;

    //audiosource refrences
    AudioSource uiAS;
    AudioSource gameplayAS;

    [Header("Sounds Data")]
    [SerializeField] SoundsData SoundsData;



    private void Awake()
    {
        if (!AM_Instance)
        {
            AM_Instance = this;
        }

        uiAS = transform.GetChild(0).GetComponent<AudioSource>();
        gameplayAS = transform.GetChild(2).GetComponent<AudioSource>();
    }

    #region All Play Sound Functions


    //****************************************************************** GamePlay *********************************************************************//

    public void PlayNormalCrimeCarPopUp()
    {        
        if (SoundsData.NC_PopUp == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.NC_PopUp);
    }

    public void PlayNormalCrimeCarPopUpTap()
    {
        if (SoundsData.NC_PopUpTap == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.NC_PopUpTap);
    }

    public void PlayMainCrimeTap()
    {
        if (SoundsData.MainCrimeTapOnCar == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.MainCrimeTapOnCar);
    }

    public void PlaySpecEnter()
    {
        if (SoundsData.spec_Enter == null) return;
        PlayOneShotClip(ClipType.Gameplay , SoundsData.spec_Enter);
    }

    public void PlayMainCrimeNoTap()
    {
        if (SoundsData.MainCrimeNoTap == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.MainCrimeNoTap);
    }

    public void PlayTap_NTC()
    {
        if (SoundsData.Tap_NTC == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.Tap_NTC);
    }

    public void PlaySignLaser()
    {
        if (SoundsData.SignLaserSound == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.SignLaserSound);
    }

    public void PlayMultiTapFire()
    {
        if (SoundsData.MultiTap_Fire == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.MultiTap_Fire);
    }

    public void PlayPoliceCome()
    {
        if (SoundsData.PoliceCome == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.PoliceCome);
    }

    public void PlayPoliceGo()
    {
        if (SoundsData.PoliceGo == null) return;
        PlayOneShotClip(ClipType.Gameplay, SoundsData.PoliceGo);
    }


    //************************************************************ UISounds ***************************************************************//

    
    public void PlayUiClick()
    {
        if (SoundsData.UiClick == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.UiClick);
    }

    public void PlayItemPick()
    {
        if (SoundsData.ItemPick == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.ItemPick);
    }

    public void PlayItemDrop()
    {
        if (SoundsData.ItemDrop == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.ItemDrop);
    }

    public void PlayItemOpenSound()
    {
        if (SoundsData.ItemOpened == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.ItemOpened);
    }    

    public void PlayAzhir()
    {
        if (SoundsData.AzhirSound == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.AzhirSound);
    }

    public void PlayArrowUp()
    {
        if (SoundsData.ArrowUp == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.ArrowUp);
    }

    public void PlayScrollSign()
    {
        if (SoundsData.ScrollSign == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.ScrollSign);
    }

    public void PlayWinSound()
    {
        if (SoundsData.WinSound == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.WinSound);
    }

    public void PlayWrongItemPlacement()
    {
        if (SoundsData.ItemWrongPalcement == null) return;
        PlayOneShotClip(ClipType.UI, SoundsData.ItemWrongPalcement);
    }

    #endregion


    #region General Functions

    public void PlayOneShotClip(ClipType type , Sound sound)
    {
        PlayOneShotClip(type , sound.clip , sound.volume);
    }

    public void PlayOneShotClip(ClipType type, AudioClip clip)
    {
        PlayOneShotClip(type, clip, 1f);
    }

    public void PlayOneShotClip(ClipType type, AudioClip clip, float volume)
    {
        switch (type)
        {
            case ClipType.UI:
                BolBolInGameTools.playOneShotFixed(uiAS, clip, volume);
                break;
            case ClipType.Gameplay:
                BolBolInGameTools.playOneShotFixed(gameplayAS, clip, volume);
                break;
            default:
                break;
        }
    }

    public void PlaySound(ClipType type, AudioClip clip, float volume, bool isLoop)
    {
        switch (type)
        {
            case ClipType.UI:
                BolBolInGameTools.PlaySoundFixed(uiAS, clip, volume, isLoop);
                break;
            case ClipType.Gameplay:
                BolBolInGameTools.PlaySoundFixed(gameplayAS, clip, volume, isLoop);
                break;
            default:
                break;
        }
    }

    #endregion

    private void OnDestroy()
    {
        AM_Instance = null;
    }
}





