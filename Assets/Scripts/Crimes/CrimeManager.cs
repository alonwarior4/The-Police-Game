using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CrimeType
{
    None , NoCrime, SideCrime, MainCrime
}

public class CrimeManager : MonoBehaviour
{
    public static Crime GetCrimeByCrimeType(CrimeType crimeType, CrimeCar crimeCar)
    {
        switch (crimeType)
        {
            case CrimeType.None:
                return null;
            case CrimeType.NoCrime:
                return new NoCrime(crimeCar);
            case CrimeType.SideCrime:
                return new RandomCrime(crimeCar);
            case CrimeType.MainCrime:
                return new MainCrime(crimeCar);
            default:
                return null;
        }
    }
}
