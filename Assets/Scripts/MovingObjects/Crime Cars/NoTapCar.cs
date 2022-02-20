using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoTapCar : SpecialCar
{
    [Header("player punish when tap on car")]
    [SerializeField] int tapPunishValue;


    public override void CheckSpecTutorial()
    {
        Tutorial.t_Instance.CheckFirstTimeNoTapCar(transform);
    }

    public override void PlayerPunish() { return; }    

    public override void OnPointerDown(PointerEventData eventData)
    {
        GameManager.GM_Instance.AllowedCars -= tapPunishValue;
        AudioManager.AM_Instance.PlayTap_NTC();
    }

}
