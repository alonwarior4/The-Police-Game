using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvisibleCar : SpecialCar
{
    [Header("disable laser time")]
    public float LaserTime;
    int shieldBoolId => Animator.StringToHash("Shield");


    public override void DoSomeThingBeforeResapwn()
    {
        movingObjectAnim.SetBool(shieldBoolId, true);
    }
}
