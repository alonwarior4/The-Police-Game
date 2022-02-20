using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCrime : Crime
{
    public NoCrime(CrimeCar crimeCar) : base(crimeCar)
    {

    }

    public override void CrimeWork()
    {        
        FadeInCarSprites();
    }

    public override void TouchWork() { return; }   

    public void FadeInCarSprites()
    {
        for (int i = 0; i < targetCrimeCar.carSprites.Length; i++)
        {
            targetCrimeCar.carSprites[i].color = Color.white;
        }
    }
}
