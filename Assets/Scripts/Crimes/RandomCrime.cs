using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomCrime : Crime
{
    int popUpBool => Animator.StringToHash("popUpChange");
    ContinueTutoActionState sideTapState = ContinueTutoActionState.TapSideCrimeCar;

    public RandomCrime(CrimeCar crimeCar) : base(crimeCar)
    {

    }

    public override void CrimeWork()
    {     
        CrimeCoroutine = GameManager.GM_Instance.StartCoroutine(SideCrimeWorkCoroutine());
    }

    public override void TouchWork()
    {        
        if (CrimeCoroutine != null)
        {
            GameManager.GM_Instance.StopCoroutine(CrimeCoroutine);
        }

        TouchWhenSideCrime();        
    }

    IEnumerator SideCrimeWorkCoroutine()
    {
        NormalCrimeCar normalCrimeCar = targetCrimeCar as NormalCrimeCar;

        AudioManager.AM_Instance.PlayNormalCrimeCarPopUp();

        normalCrimeCar.isDoingSomeCrime = true;
        Sprite randomCrime_SP = normalCrimeCar.randomSprites.GetRandomSpriteFromList();
        normalCrimeCar.randomSpritePlace.sprite = randomCrime_SP;
        normalCrimeCar.popUpAnim.SetBool(popUpBool, true);

        WaitForSeconds waitToSideTime = new WaitForSeconds(normalCrimeCar.side_TapAllowedTime);
        yield return waitToSideTime;

        normalCrimeCar.currentCamera = null;
        normalCrimeCar.popUpAnim.SetBool(popUpBool, false);
        normalCrimeCar.isDoingSomeCrime = false;
        normalCrimeCar.CurrentCrime = new NoCrime(normalCrimeCar);
    }

    void TouchWhenSideCrime()
    {
        Tutorial.t_Instance.CheckContinueState(sideTapState);


        NormalCrimeCar normalCrimeCar = targetCrimeCar as NormalCrimeCar;

        AudioManager.AM_Instance.PlayNormalCrimeCarPopUpTap();

        normalCrimeCar.SidePunish();
        normalCrimeCar.isDoingSomeCrime = false;

        normalCrimeCar.currentCamera.SideCrimeNumber++;
        normalCrimeCar.currentCamera = null;

        normalCrimeCar.popUpAnim.SetBool(popUpBool, false);
        normalCrimeCar.CurrentCrime = new NoCrime(normalCrimeCar);
    }
}
