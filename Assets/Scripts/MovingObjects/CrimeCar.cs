using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class CrimeCar : Vehicle , IPointerDownHandler
{
    [Header("allowed cars minus when tap")]
    [SerializeField] protected int noTapPunishValue;

    [Header("punish value for crimes")]
    [SerializeField] protected float mainPunishValue;

    [Header("Time Allowed to Tap On Car")]
    public float main_TapAllowedTime;

    [Header("Is car doing any crime")]
    public bool isDoingSomeCrime = false;

    [Header("Parts of vehicle to fade out")]
    public SpriteRenderer[] carSprites;

    [Header("Chance to crime")]
    [Range(0f, 1f)] public float mainCrimeChance;
    [HideInInspector] public Sign currentSign;

    public bool isCanTouch;
    public Action TouchWork;

    Crime currentCrime;
    public Crime CurrentCrime
    {
        get
        {
            return currentCrime;
        }
        set
        {
            currentCrime = value;
            currentCrime.CrimeWork();
            TouchWork = currentCrime.TouchWork;
        }
    }
    

    protected override void Start()
    {
        isCanTouch = true;
        base.Start();
        //moveSM.IntializeStateMachine(movingState);
        CurrentCrime = new NoCrime(this);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!isCanTouch) return;
        TouchWork?.Invoke();
    }

    public virtual void PlayerPunish() 
    {
        GameManager.GM_Instance.AllowedCars -= noTapPunishValue;
        AudioManager.AM_Instance.PlayMainCrimeNoTap();
    }
    public void MainPunish() { GameManager.GM_Instance.TotalMoney += mainPunishValue; }
    public virtual void MainCrimeCollider(Sign sign) { }    
    
}
