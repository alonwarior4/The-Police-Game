using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCrime : Crime
{
    int TicketAnimId => Animator.StringToHash("Ticket");
    //ContinueTutoActionState mainTapState = ContinueTutoActionState.TapMainCrimeCar;

    public MainCrime(CrimeCar crimeCar) : base(crimeCar)
    {

    }

    public override void CrimeWork()
    {
        CrimeCoroutine = GameManager.GM_Instance.StartCoroutine(MainCrimeWorkCoroutine());
    }

    public override void TouchWork()
    {
        if (CrimeCoroutine != null)
        {
            GameManager.GM_Instance.StopCoroutine(CrimeCoroutine);
        }

        GameManager.GM_Instance.StartCoroutine(TouchWhenMainCrimeCoroutine());
    }

    IEnumerator MainCrimeWorkCoroutine()
    {
        Azhir.azhir_Instance.Azhir_AddToMomentCrimeCars();

        targetCrimeCar.isDoingSomeCrime = true;
        targetCrimeCar.currentSign.isMainCrimeAvailable = false;

        WaitForSeconds WaitToCarTapAllowedTime = new WaitForSeconds(targetCrimeCar.main_TapAllowedTime);
        yield return WaitToCarTapAllowedTime;

        targetCrimeCar.isCanTouch = false;

        yield return GameManager.GM_Instance.StartCoroutine(FadeOutCarSprites());

        Azhir.azhir_Instance.Azhir_RemoveFromMomentCrimeCars();
        targetCrimeCar.carCollider.enabled = false;

        if(targetCrimeCar as NoTapCar)
        {
            targetCrimeCar.currentSign.TapCarInCurrentSign(true);
        }
        else
        {
            //targetCrimeCar.currentSign.isMainCrimeAvailable = true;
            targetCrimeCar.currentSign.CheckForOpenSign();
        }
        targetCrimeCar.PlayerPunish();
        targetCrimeCar.currentSign.RemoveFromMainCrimeCars(targetCrimeCar);
        targetCrimeCar.moveSM.ChangeState(targetCrimeCar.stopingState);
        targetCrimeCar.transform.position = GameManager.GM_Instance.tappedCarGarage.position;


        if (targetCrimeCar as NormalCrimeCar)
        {
            SpawnManager.SM_Intance.AddToRespawnableCars(targetCrimeCar);
        }
        if(targetCrimeCar as SpecialCar)
        {
            SpecialCarManager.SCM_Instance.isSpecialCarInScene = false;
        }
    }

    IEnumerator FadeOutCarSprites()
    {
        float fadeTime = GameManager.GM_Instance.vehicleFadeTime;

        // Cash Refrence
        WaitForEndOfFrame waitToEndFrame = new WaitForEndOfFrame();

        Color adjective = Color.white;
        Color transparent = new Color(1, 1, 1, 0);

        for (float f = 0; f < fadeTime; f += Time.unscaledDeltaTime)
        {
            for (int i = 0; i < targetCrimeCar.carSprites.Length; i++)
            {
                targetCrimeCar.carSprites[i].color = Color.Lerp(adjective, transparent, Mathf.Min(f / fadeTime, 1));
            }

            yield return waitToEndFrame;
        }
    }

    IEnumerator TouchWhenMainCrimeCoroutine()
    {
        ContinueTutoActionState continueState = ContinueTutoActionState.TapMainCrimeCar;
        if(targetCrimeCar as MultiTapCar)
        {
            continueState = ContinueTutoActionState.TapMultiTapCar;
        }

        Tutorial.t_Instance.CheckContinueState(continueState);

        AudioManager.AM_Instance.PlayMainCrimeTap();
        Azhir.azhir_Instance.Azhir_RemoveFromMomentCrimeCars();
        targetCrimeCar.isCanTouch = false;

        targetCrimeCar.moveSM.ChangeState(targetCrimeCar.stopingState);

        targetCrimeCar.MainPunish();
        targetCrimeCar.movingObjectAnim.SetTrigger(TicketAnimId);
        if(targetCrimeCar as SpecialCar)
        {
            targetCrimeCar.currentSign.TapCarInCurrentSign(true);            
        }
        else
        {
            targetCrimeCar.currentSign.TapCarInCurrentSign(false);
        }

        targetCrimeCar.currentSign.RemoveFromMainCrimeCars(targetCrimeCar);

        GameManager.GM_Instance.TappedCarsNumber++;

        yield return new WaitForEndOfFrame();
        WaitForSecondsRealtime WaitForCurrentAnimation = new WaitForSecondsRealtime( 1.1f);
        yield return WaitForCurrentAnimation;

        yield return GameManager.GM_Instance.StartCoroutine(FadeOutCarSprites());

        targetCrimeCar.carCollider.enabled = false; 

        targetCrimeCar.transform.position = GameManager.GM_Instance.tappedCarGarage.position;

        if(targetCrimeCar as SpecialCar)
        {
            SpecialCarManager.SCM_Instance.isSpecialCarInScene = false;
        }
        else
        {
            SpawnManager.SM_Intance.AddToRespawnableCars(targetCrimeCar);
        }
    }
}
